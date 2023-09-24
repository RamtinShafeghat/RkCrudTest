namespace Mc2.CrudTest.Application.Exceptions;

public class BadRequestException: Exception
{
    public BadRequestException(string message): base(message)
    {

    }
}
