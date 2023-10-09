using System.Linq.Expressions;

namespace Mc2.CrudTest.Persistence.Repositories;

internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(IRayanKarDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistAsync(Expression<Func<Customer, bool>> condition) 
        => await this.dbContext.Customers.Where(condition).AnyAsync();
}
