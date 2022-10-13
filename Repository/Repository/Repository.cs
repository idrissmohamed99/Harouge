using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Delete(TEntity entity)
        {
            await Task.FromResult(true);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public async Task DeleteAll(List<TEntity> entity)
        {
            await Task.FromResult(true);
            _dbContext.RemoveRange(entity);
        }
        public async Task<bool> Remove(TEntity entity)
        {
            await Task.FromResult(true);
            _dbContext.Remove(entity);
            return true;
        }

        public async Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            await Task.FromResult(true);
            IQueryable<TEntity> result = _dbContext.Set<TEntity>().Where(predicate);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            await Task.FromResult(true);
            return _dbContext.Set<TEntity>().ToList();
        }

        public async Task<TEntity> GetByID(string Id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(Id);
        }

        public async Task<bool> Insert(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            return true;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(TEntity entity)
        {
            await Task.FromResult(true);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<bool> InsertList(List<TEntity> entity)
        {
            await _dbContext.AddRangeAsync(entity);
            return true;
        }

        public async Task<int> GetCount()
        {
            return await _dbContext.Set<TEntity>().CountAsync() + 1;
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().CountAsync(predicate) + 1;
        }
    }
}
