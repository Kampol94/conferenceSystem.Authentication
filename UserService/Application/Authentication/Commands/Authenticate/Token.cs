using UserService.Domain.Contracts;

namespace UserService.Application.Authentication.Commands.Authenticate;
public class Token : ValueObject
{
    public string Value { get; }

	public Token(string value)
	{
        Value = value;
	}

	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}
