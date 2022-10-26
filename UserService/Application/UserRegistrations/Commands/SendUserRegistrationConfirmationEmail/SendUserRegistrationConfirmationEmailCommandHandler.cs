using MediatR;
using UserService.Application.Contracts;
using UserService.Application.Contracts.Commands;

namespace UserService.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

internal class SendUserRegistrationConfirmationEmailCommandHandler : ICommandHandler<SendUserRegistrationConfirmationEmailCommand>
{
    private readonly IEmailSender _emailSender;

    public SendUserRegistrationConfirmationEmailCommandHandler(
        IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task<Unit> Handle(SendUserRegistrationConfirmationEmailCommand command, CancellationToken cancellationToken)
    {
        string link = $"<a href=\"{command.ConfirmLink}{command.UserRegistrationId.Value}\">link</a>";

        string content = $"Welcome to MyMeetings application! Please confirm your registration using this {link}.";

        var emailMessage = new EmailMessage(
            command.Email,
            "MyMeetings - Please confirm your registration",
            content);

        _emailSender.SendEmail(emailMessage);

        return Unit.Task;
    }
}