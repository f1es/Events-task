namespace Events.Domain.Exceptions;

public class FailedToLoginException : Exception
{
    public FailedToLoginException(string message)
        : base(message)
    {
        
    }
}
