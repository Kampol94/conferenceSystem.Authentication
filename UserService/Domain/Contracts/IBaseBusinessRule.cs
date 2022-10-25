namespace UserService.Domain.Contracts;

public interface IBaseBusinessRule
{
    bool IsBroken();

    string Message { get; }
}