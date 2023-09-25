namespace Mc2.CrudTest.SharedKernel;

public interface ITransactionCenter
{
    Task RunInside(Func<Task> action);
}
