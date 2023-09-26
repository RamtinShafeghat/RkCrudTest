using Mc2.CrudTest.Application.UnitTests.Mocks;

namespace Mc2.CrudTest.Application.UnitTests.Customers.Commands;

public class DeleteCustomerCommandTest
{
    private readonly Mock<ICustomerEventStore> mockEventStore;
    private readonly Mock<ICustomerRepository> mockRepository;

    public DeleteCustomerCommandTest()
    {
        mockEventStore = RepositoryMocks.GetEmptyEventStore();
        mockRepository = RepositoryMocks.GetCustomerRepository(true);
    }

    [Fact]
    public async Task Delete_Existing_Customer_By_Command()
    {
        var handler = new DeleteCustomerCommandHandler(mockEventStore.Object, mockRepository.Object);

        var customer = (await mockRepository.Object.ListAllAsync()).First();

        await handler.Handle(new DeleteCustomerCommand() { Id = customer.Id }, CancellationToken.None);

        var deletedCustomer = (await mockRepository.Object.ListAllAsync()).First();
        deletedCustomer.IsDeleted.ShouldBe(true);
    }
}