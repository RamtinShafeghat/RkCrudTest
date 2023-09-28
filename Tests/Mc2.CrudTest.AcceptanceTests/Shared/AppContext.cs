using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Shared;

public class AppContext
{
    public IServiceProvider Container;
    public IRayanKarDbContext DatabaseContext;

    public AppContext()
    {
        var options = new DbContextOptionsBuilder<MockDatabaseContext>()
                            .UseInMemoryDatabase(databaseName: "Mc2InMemory")
                            .Options;
        DatabaseContext = new MockDatabaseContext(options);

        var provider = new ServiceCollection()
                            .AddApplicationServices()
                            .AddPersistenceServices(null, false)
                            .AddInfrastructureServices()
                            .AddSingleton(_ => DatabaseContext)
                            .BuildServiceProvider();

        Container = provider;
    }
}