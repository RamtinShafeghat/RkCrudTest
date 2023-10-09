namespace Mc2.CrudTest.Application.Features.Customers;

public class CustomerCommand
{
    public CustomerCommandDto Dto { get; set; }
}


public class CustomerCommandDto
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
}
