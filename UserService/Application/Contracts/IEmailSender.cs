using UserService.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

namespace UserService.Application.Contracts;
public interface IEmailSender
{
    void SendEmail(EmailMessage emailMessage);
}
