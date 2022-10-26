namespace UserService.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

public class EmailMessage
{
    private string _email;
    private string _v;
    private string _content;

    public EmailMessage(string email, string v, string content)
    {
        _email = email;
        _v = v;
        _content = content;
    }
}