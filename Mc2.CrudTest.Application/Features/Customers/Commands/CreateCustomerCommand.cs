namespace Mc2.CrudTest.Application.Features.Customers.Commands;

public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get;  set; }
    public string BankAccountNumber { get; set; }
}
