using Moq.AutoMock;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests.Shared;

public class AppContext
{
    public AutoMocker Mocker;
    public IServiceProvider Container;
    public IRayanKarDbContext DatabaseContext;

    public AppContext()
    {
        Mocker = new AutoMocker();

        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Mc2*.dll");
        var assemblies = files.Select(p => AssemblyLoadContext.Default.LoadFromAssemblyPath(p));

        var options = new DbContextOptionsBuilder<MockDatabaseContext>()
                            .UseInMemoryDatabase(databaseName: "Mc2InMemory")
                            .Options;
        DatabaseContext = new MockDatabaseContext(options);

        var provider = new ServiceCollection()
                            .Scan(p => p.FromAssemblies(assemblies)
                                        .AddClasses()
                                        .AsMatchingInterface())
                            .AddApplicationServices()
                            .AddPersistenceServices(null)
                            .AddInfrastructureServices()
                            .AddSingleton(_ => DatabaseContext)
                            .BuildServiceProvider();

        Container = provider;
    }
}