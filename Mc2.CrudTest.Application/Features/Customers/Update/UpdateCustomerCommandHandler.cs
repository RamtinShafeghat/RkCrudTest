using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers.Update;

public class UpdateCustomerCommandHandler :
    IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly ICustomerEventStore customerEventStore;
    private readonly ICustomerRepository customerRepository;

    public UpdateCustomerCommandHandler(
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.customerEventStore = customerEventStore;
        this.customerRepository = customerRepository;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(
        UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateCustomerCommandResponse();

        var validator = new UpdateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Update Failed";
            response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }

        if (response.Success)
        {
            var customer = await this.customerEventStore.RehydreateAsync(request.Id.ToString());

            Customer.UpdateCustomer(
                customer,
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.Email,
                request.PhoneNumber,
                request.BankAccountNumber);

            await customerEventStore.SaveAsync(customer);
            await customerRepository.UpdateAsync(customer);
        }

        return response;
    }
}
