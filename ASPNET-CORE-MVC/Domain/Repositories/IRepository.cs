using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> Get(int id);

    Task<TEntity?> Single(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> GetAll();

    Task<IEnumerable<TEntity>?> Find(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<bool>> Select(Expression<Func<TEntity, bool>> predicate);

    Task Add(TEntity entity);

    Task Update(TEntity entity);

    Task Remove(TEntity entity);
}