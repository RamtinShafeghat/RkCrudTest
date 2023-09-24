using AutoMapper;
using Mc2.CrudTest.Application.Contracts.Persistence;
using Mc2.CrudTest.Core.CustomerAggregate;
using MediatR;

namespace Mc2.CrudTest.Application.Features.Customers.Commands;

public class CreateCustomerCommandHandler : 
    IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ICustomerRepository customerRepository;
    private readonly ICustomerEventRepository customerEventRepository;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerRepository customerRepository,
        ICustomerEventRepository eventStoreRepository)
    {
        this.mapper = mapper;
        this.customerEventRepository = eventStoreRepository;
        this.customerRepository = customerRepository;
    }

    public async Task<CreateCustomerCommandResponse> Handle(
        CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerReponse = new CreateCustomerCommandResponse();

        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            customerReponse.Success = false;
            customerReponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                customerReponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        if (customerReponse.Success)
        {
            var customer = Customer.CreateCustomer(
                request.FirstName, 
                request.LastName, 
                request.DateOfBirth, 
                request.Email, 
                request.BankAccountNumber);

            await this.customerEventRepository.SaveAsync(customer);
            customer = await this.customerRepository.AddAsync(customer);
            
            customerReponse.Customer = this.mapper.Map<CreateCustomerDto>(customer);
        }

        return customerReponse;
    }
}
