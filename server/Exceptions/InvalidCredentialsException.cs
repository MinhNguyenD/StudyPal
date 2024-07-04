using System.Runtime.Serialization;

namespace server.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
    {
    }

    public InvalidCredentialsException(string? message) : base(message)
    {
    }

    public InvalidCredentialsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
