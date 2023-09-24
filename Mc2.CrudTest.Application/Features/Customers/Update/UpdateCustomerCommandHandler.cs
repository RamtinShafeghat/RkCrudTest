using Mc2.CrudTest.Application.Contracts.Persistence;
using Mc2.CrudTest.Application.Exceptions;

namespace Mc2.CrudTest.Application.Features.Customers.Update;

public class UpdateCustomerCommandHandler :
    IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ICustomerEventStore customerEventStore;
    private readonly ICustomerRepository customerRepository;

    public UpdateCustomerCommandHandler(
        IMapper mapper,
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.mapper = mapper;
        this.customerEventStore = customerEventStore;
        this.customerRepository = customerRepository;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(
        UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateCustomerCommandResponse();

        var validator = new UpdateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Update Failed";
            response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }

        if (response.Success)
        {
            var customer = await this.customerEventStore.RehydreateAsync(request.Id.ToString());
            customer.ValidateExistence(request.Id);

            var customerDto = this.mapper.Map<Customer.Dto>(request.Dto);
            Customer.UpdateCustomer(customer, customerDto);

            await customerEventStore.SaveAsync(customer);
            await customerRepository.UpdateAsync(customer);
        }

        return response;
    }
}
