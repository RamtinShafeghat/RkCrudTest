using System.Linq.Expressions;

namespace Mc2.CrudTest.Application.Contracts.Persistence;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<bool> ExistAsync(Expression<Func<Customer, bool>> condition);
}
