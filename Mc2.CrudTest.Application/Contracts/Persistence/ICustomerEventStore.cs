using Mc2.Crud.SharedKernel.Contracts;
using Mc2.CrudTest.Core.CustomerAggregate;

namespace Mc2.CrudTest.Application.Contracts.Persistence;

public interface ICustomerEventStore : IAggregateEventStore<Customer>
{
}
