using Domain.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ServerDbContext _context;
    public IUserRepository Users { get; }
    public ICategoryRepository Categories { get; }
    public ITransactionRepository Transactions { get; }

    private bool _disposed = false;

    public UnitOfWork(ServerDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Categories = new CategoryRepository(_context);
        Transactions = new TransactionsRepository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}