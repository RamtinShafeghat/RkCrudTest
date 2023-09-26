using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class UpdateCustomerCommandHandler :
    IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ICustomerEventStore eventStore;
    private readonly ICustomerRepository repository;
    private readonly UpdateCustomerCommandValidator validator;

    public UpdateCustomerCommandHandler(
        IMapper mapper,
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository,
        UpdateCustomerCommandValidator validator)
    {
        this.mapper = mapper;
        this.eventStore = customerEventStore;
        this.repository = customerRepository;
        this.validator = validator;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(
        UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateCustomerCommandResponse();
        await Validate(request, response, cancellationToken);

        if (response.Success)
        {
            var customer = await this.eventStore.RehydreateAsync(request.Id.ToString());
            customer.ValidateExistence(request.Id);

            var customerDto = this.mapper.Map<Customer.Dto>(request.Dto);
            Customer.Update(customer, customerDto);

            var t = await this.eventStore.GetTransaction();
            await t.RunInside(async () =>
            {
                await eventStore.SaveAsync(customer);
                await repository.UpdateAsync(customer);
            });
        }

        return response;
    }

    private async Task Validate(
        UpdateCustomerCommand request, 
        UpdateCustomerCommandResponse response, 
        CancellationToken cancellationToken)
    {
        if (validator != default)
        {
            var validationResult = await this.validator.ValidateAsync(request.Dto, cancellationToken);
            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.Message = "Update Failed";
                response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
        }
    }
}
