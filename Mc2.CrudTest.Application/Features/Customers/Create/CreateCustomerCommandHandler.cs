using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class CreateCustomerCommandHandler :
    IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ICustomerEventStore eventStore;
    private readonly ICustomerRepository repository;
    private readonly CreateCustomerCommandValidator validator;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository,
        CreateCustomerCommandValidator validator)
    {
        this.mapper = mapper;
        this.eventStore = customerEventStore;
        this.repository = customerRepository;
        this.validator = validator;
    }

    public async Task<CreateCustomerCommandResponse> Handle(
        CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateCustomerCommandResponse();
        await Validate(request, response, cancellationToken);

        if (response.Success)
        {
            var customerDto = this.mapper.Map<Customer.Dto>(request.Dto);
            var customer = Customer.Create(customerDto);

            var t = await this.eventStore.GetTransaction();
            await t.RunInside(async () =>
            {
                await eventStore.SaveAsync(customer);
                customer = await repository.AddAsync(customer);
            });

            response.Id = customer.Id;
        }

        return response;
    }

    private async Task Validate(
        CreateCustomerCommand request, 
        CreateCustomerCommandResponse response, 
        CancellationToken cancellationToken)
    {
        if (this.validator != default)
        {
            var validationResult = await this.validator.ValidateAsync(request.Dto, cancellationToken);
            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.Message = "Create Failed";
                response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
        }
    }
}
