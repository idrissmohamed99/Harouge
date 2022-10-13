using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Insert(TEntity entity);
        Task<bool> InsertList(List<TEntity> entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Remove(TEntity entity);
        Task DeleteAll(List<TEntity> entity);
        Task<TEntity> GetByID(string Id);
        Task<int> GetCount();
        Task<int> GetCount(Expression<Func<TEntity, bool>> predicate);
        Task SaveChanges();
    }
}
