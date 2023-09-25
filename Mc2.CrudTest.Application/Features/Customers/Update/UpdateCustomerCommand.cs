namespace Mc2.CrudTest.Application.Features.Customers;

public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResponse>
{
    public Guid Id { get; set; }

    public CustomerCommandDto Dto { get; set; }
}
