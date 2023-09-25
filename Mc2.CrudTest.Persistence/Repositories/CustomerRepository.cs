namespace Mc2.CrudTest.Persistence.Repositories;

internal class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(RayanKarDbContext dbContext) : base(dbContext)
    {
    }

    protected override async Task SaveChangesAsync()
	{
		try
		{
            await base.SaveChangesAsync();
        }
		catch (Exception ex)
		{
            if (ex.InnerException.Message.Contains($"IX_{nameof(Customer)}s_{nameof(Customer.Email)}"))
                throw new ArgumentException("Email has already in use");
            if (ex.InnerException.Message.Contains($"IX_{nameof(Customer)}s_{nameof(Customer.FirstName)}_{nameof(Customer.LastName)}_{nameof(Customer.DateOfBirth)}"))
                throw new ArgumentException("Combination of FirstName, LastName and DateOfBirth is in use");
            throw;
		}
    }
}
