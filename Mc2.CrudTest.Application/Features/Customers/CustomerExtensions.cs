using Mc2.CrudTest.Application.Exceptions;

namespace Mc2.CrudTest.Application.Features.Customers;

internal static class CustomerExtensions
{
    public static void ValidateExistence(this Customer customer, Guid id)
    {
        if (customer == null)
            throw new NotFoundException(nameof(customer), id);
        if (customer.IsDeleted)
            throw new BadRequestException($"Customer {id} has deleted");
    }
}
