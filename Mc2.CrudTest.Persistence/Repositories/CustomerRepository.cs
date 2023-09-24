namespace Mc2.CrudTest.Persistence.Repositories;

internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(RayanKarDbContext dbContext) : base(dbContext)
    {
    }
}
