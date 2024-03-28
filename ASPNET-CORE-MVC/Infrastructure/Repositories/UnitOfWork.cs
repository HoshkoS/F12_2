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

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            await _context.DisposeAsync();
        }

        _disposed = true;
    }

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }
}