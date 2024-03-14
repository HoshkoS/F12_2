using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionsRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionsRepository(DbContext context) : base(context)
    {
    }
}