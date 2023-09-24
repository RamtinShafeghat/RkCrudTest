using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers.Create;

public class CreateCustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly ICustomerEventStore customerEventStore;
    private readonly ICustomerRepository customerRepository;

    public CreateCustomerCommandHandler(
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.customerEventStore = customerEventStore;
        this.customerRepository = customerRepository;
    }

    public async Task<CreateCustomerCommandResponse> Handle(
        CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateCustomerCommandResponse();

        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Create Failed";
            response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }

        if (response.Success)
        {
            var customer = Customer.CreateCustomer(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.Email,
                request.BankAccountNumber);

            await customerEventStore.SaveAsync(customer);
            customer = await customerRepository.AddAsync(customer);

            response.Id = customer.Id;
        }

        return response;
    }
}
