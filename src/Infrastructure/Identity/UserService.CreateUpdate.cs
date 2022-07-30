using AuthenticationService.Application.Common.Exceptions;
using AuthenticationService.Application.Identity;
using AuthenticationService.Application.Identity.Users;
using AuthenticationService.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace AuthenticationService.Infrastructure.Identity;

internal partial class UserService
{
    /// <summary>
    /// This is used when authenticating with AzureAd.
    /// The local user is retrieved using the objectidentifier claim present in the ClaimsPrincipal.
    /// If no such claim is found, an InternalServerException is thrown.
    /// If no user is found with that ObjectId, a new one is created and populated with the values from the ClaimsPrincipal.
    /// If a role claim is present in the principal, and the user is not yet in that roll, then the user is added to that role.
    /// </summary>
    public async Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if (string.IsNullOrWhiteSpace(objectId))
        {
            throw new InternalServerException("Invalid objectId");
        }

        var user = await _userManager.Users.Where(u => u.ObjectId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if (principal.FindFirstValue(ClaimTypes.Role) is string role &&
            await _roleManager.RoleExistsAsync(role) &&
            !await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
        {
            throw new InternalServerException(string.Format("Username or Email not valid."));
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
        {
            throw new InternalServerException(string.Format("Username {0} is already taken.", username));
        }

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
            {
                throw new InternalServerException(string.Format("Email {0} is already taken.", email));
            }
        }

        IdentityResult? result;
        if (user is not null)
        {
            user.ObjectId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);
        }
        else
        {
            user = new ApplicationUser
            {
                ObjectId = principal.GetObjectId(),
                FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = principal.FindFirstValue(ClaimTypes.Surname),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            result = await _userManager.CreateAsync(user);
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.", result.GetErrors());
        }

        return user;
    }

    public async Task<string> CreateAsync(CreateUserRequest request, string origin)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.", result.GetErrors());
        }

        var messages = new List<string> { string.Format("User {0} Registered.", user.UserName) };

        return string.Join(Environment.NewLine, messages);
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException("User Not Found.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        if (!result.Succeeded)
        {
            throw new InternalServerException("Update profile failed", result.GetErrors());
        }
    }
}
