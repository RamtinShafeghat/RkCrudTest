namespace Mc2.CrudTest.SharedKernel.Contracts;

public interface ITransactionCenter
{
    Task RunInside(Func<Task> action);
}
