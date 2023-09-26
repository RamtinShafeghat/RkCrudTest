namespace Mc2.CrudTest.Application.UnitTests.Mocks;

public class RepositoryMocks
{
    private const string firstName = "ali";
    private const string lastName = "taheri";
    private const string email = "alit@email.com";
    private const string phoneNumber = "09123456987";
    private const string bankAccountNumber = "IR123456789";
    private static readonly DateOnly dateOfBirth = new(1990, 01, 02);

    public static Customer GetCustomer()
    {
        return Customer.Create(new Customer.Dto
        (
            FirstName: firstName,
            LastName: lastName,
            DateOfBirth: dateOfBirth,
            Email: email,
            PhoneNumber: phoneNumber,
            BankAccountNumber: bankAccountNumber
        ));
    }
    public static CustomerCommandDto GetCustomerCommandDto()
    {
        return new CustomerCommandDto
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Email = email,
            PhoneNumber = phoneNumber,
            BankAccountNumber = bankAccountNumber
        };
    }

    public static Mock<ICustomerRepository> GetCustomerRepository(bool withData)
    {
        List<Customer> customers = new();
        if (withData)
        {
            customers.Add(GetCustomer());
        }

        var mockRepository = new Mock<ICustomerRepository>();

        mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                      .Returns((Guid id) => Task.FromResult(customers.FirstOrDefault(e => e.Id == id)));
        
        mockRepository.Setup(repo => repo.ListAllAsync())
                      .ReturnsAsync(customers);

        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(
            (Customer c) =>
            {
                customers.Add(c);
                return c;
            });

        mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Customer>()))
            .Callback((Customer customer) =>
            {
                customers.Clear();
                customers.Add(customer);
            });

        return mockRepository;
    }
    public static Mock<ICustomerEventStore> GetEmptyEventStore()
    {
        var mockEventStore = new Mock<ICustomerEventStore>();

        var mockContextTransaction = new Mock<ITransactionCenter>();
        mockContextTransaction.Setup(a => a.RunInside(It.IsAny<Func<Task>>())).Callback(async (Func<Task> t) =>
        {
            await t();
        });

        mockEventStore.Setup(r => r.GetTransaction()).ReturnsAsync(mockContextTransaction.Object);
        mockEventStore.Setup(r => r.SaveAsync(It.IsAny<Customer>())).Callback((Customer c) => { });
        mockEventStore.Setup(r => r.RehydreateAsync(It.IsAny<string>())).ReturnsAsync((string s) => GetCustomer());

        return mockEventStore;
    }
}
