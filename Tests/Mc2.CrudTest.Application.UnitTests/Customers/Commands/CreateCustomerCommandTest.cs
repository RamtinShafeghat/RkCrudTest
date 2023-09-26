using AutoMapper;
using Mc2.CrudTest.Application.UnitTests.Mocks;

namespace Mc2.CrudTest.Application.UnitTests.Customers.Commands;

public class CreateCustomerCommandTest
{
    private readonly IMapper mapper;
    private readonly Mock<ICustomerEventStore> mockEventStore;
    private readonly Mock<ICustomerRepository> mockRepository;

    public CreateCustomerCommandTest()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        mapper = configurationProvider.CreateMapper();

        mockEventStore = RepositoryMocks.GetEmptyEventStore();
        mockRepository = RepositoryMocks.GetCustomerRepository(false);
    }

    [Fact]
    public async Task Create_New_Customer_By_Command()
    {
        var handler = new CreateCustomerCommandHandler(mapper, mockEventStore.Object, mockRepository.Object, default);

        var customerDto = RepositoryMocks.GetCustomerCommandDto();

        await handler.Handle(new CreateCustomerCommand() { Dto = customerDto }, CancellationToken.None);

        var allCustomers = await mockRepository.Object.ListAllAsync();
        allCustomers.Count.ShouldBe(1);

        var customer = allCustomers.First();
        customer.Id.ShouldNotBe(default);
    }
}