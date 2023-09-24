using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers.Create;

public class CreateCustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ICustomerEventStore customerEventStore;
    private readonly ICustomerRepository customerRepository;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.mapper = mapper;
        this.customerEventStore = customerEventStore;
        this.customerRepository = customerRepository;
    }

    public async Task<CreateCustomerCommandResponse> Handle(
        CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateCustomerCommandResponse();

        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Create Failed";
            response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }

        if (response.Success)
        {
            var customerDto = this.mapper.Map<Customer.Dto>(request.Dto);
            var customer = Customer.CreateCustomer(customerDto);

            await customerEventStore.SaveAsync(customer);
            customer = await customerRepository.AddAsync(customer);

            response.Id = customer.Id;
        }

        return response;
    }
}
