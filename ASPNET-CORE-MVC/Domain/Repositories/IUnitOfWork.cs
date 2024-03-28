namespace Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
    ITransactionRepository Transactions { get; }
    Task<int> Complete();
}