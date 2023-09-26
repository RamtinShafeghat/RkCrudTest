using Microsoft.EntityFrameworkCore.Storage;

namespace Mc2.CrudTest.Persistence;

public class TransactionCenter : ITransactionCenter
{
    private readonly IDbContextTransaction transaction;

    public TransactionCenter()
    {
    }

    public TransactionCenter(IDbContextTransaction transaction)
    {
        this.transaction = transaction;
    }

    public async Task RunInside(Func<Task> action)
    {
        try
        {
            await action();
            transaction?.Commit();
        }
        catch (Exception)
        {
            transaction?.Rollback();
            throw;
        }
    }
}
