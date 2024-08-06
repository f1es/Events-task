namespace Events.Domain.Exceptions;

public class InvalidModelException : Exception
{
    public InvalidModelException(string message) 
        : base("Request model is invalid, " + message)
    {
        
    }
}
