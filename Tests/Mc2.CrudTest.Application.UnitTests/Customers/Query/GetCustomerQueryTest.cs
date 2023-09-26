using AutoMapper;
using Mc2.CrudTest.Application.UnitTests.Mocks;

namespace Mc2.CrudTest.Application.UnitTests.Customers.Query;

public class GetCustomerQueryTest
{
    private readonly IMapper mapper;
    private readonly Mock<ICustomerRepository> mockRepository;

    public GetCustomerQueryTest()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        mapper = configurationProvider.CreateMapper();

        mockRepository = RepositoryMocks.GetCustomerRepository(true);
    }

    [Fact]
    public async Task Get_Existing_Customer_By_Query()
    {
        var handler = new GetCustomerQueryHandler(mapper, mockRepository.Object);

        var customer = (await mockRepository.Object.ListAllAsync()).First();

        var fetchedCustomer = await handler.Handle(new GetCustomerQuery() { Id = customer.Id }, CancellationToken.None);

        fetchedCustomer.FirstName.ShouldBe(customer.FirstName);
        fetchedCustomer.LastName.ShouldBe(customer.LastName);
        fetchedCustomer.Email.ShouldBe(customer.Email);
        fetchedCustomer.DateOfBirth.ShouldBe(customer.DateOfBirth);
        fetchedCustomer.PhoneNumber.ShouldBe(customer.PhoneNumber);
        fetchedCustomer.bankAccountNumber.ShouldBe(customer.BankAccountNumber);
    }
}