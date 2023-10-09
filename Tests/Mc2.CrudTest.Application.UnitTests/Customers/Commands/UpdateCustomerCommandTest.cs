using AutoMapper;
using Mc2.CrudTest.Application.UnitTests.Mocks;

namespace Mc2.CrudTest.Application.UnitTests.Customers.Commands;

public class UpdateCustomerCommandTest
{
    private readonly IMapper mapper;
    private readonly Mock<ICustomerEventStore> mockEventStore;
    private readonly Mock<ICustomerRepository> mockRepository;

    public UpdateCustomerCommandTest()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        mapper = configurationProvider.CreateMapper();

        mockEventStore = RepositoryMocks.GetEmptyEventStore();
        mockRepository = RepositoryMocks.GetCustomerRepository(true);
    }

    [Fact]
    public async Task Update_Existing_Customer_By_Command()
    {
        var handler = new UpdateCustomerCommandHandler(mapper, mockEventStore.Object, mockRepository.Object);

        var customerDto = RepositoryMocks.GetCustomerCommandDto();
        customerDto.FirstName = "ramin";
        customerDto.LastName = "azizi";

        await handler.Handle(new UpdateCustomerCommand() { Dto = customerDto }, CancellationToken.None);

        var allCustomers = await mockRepository.Object.ListAllAsync();
        allCustomers.Count.ShouldBe(1);

        var customer = allCustomers.First();
        customer.FirstName.ShouldBeEquivalentTo(customerDto.FirstName);
        customer.LastName.ShouldBeEquivalentTo(customerDto.LastName);
    }
}