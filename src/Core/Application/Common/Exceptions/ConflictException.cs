using System.Net;

namespace AuthenticationService.Application.Common.Exceptions;

public class ConflictException : CustomException
{
    public ConflictException(string message)
        : base(message, null, HttpStatusCode.Conflict)
    {
    }
}