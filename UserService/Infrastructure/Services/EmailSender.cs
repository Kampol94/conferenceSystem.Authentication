using UserService.Application.Contracts;
using UserService.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

namespace UserService.Infrastructure.Services;
public class EmailSender : IEmailSender
{
    public void SendEmail(EmailMessage emailMessage)
    {
    }
}
