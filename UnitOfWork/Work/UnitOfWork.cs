using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitOfWork.Work
{

    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext, IDisposable
    {

        private Dictionary<Type, object> _dictionaryRepositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TContext Context { get; }

        public async Task<int> SaveChangeAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_dictionaryRepositories == null)
            {
                _dictionaryRepositories = new Dictionary<Type, object>();
            }

            Type type = typeof(TEntity);
            if (!_dictionaryRepositories.ContainsKey(type))
            {
                _dictionaryRepositories[type] = new Repository<TEntity>(Context);
            }

            return (IRepository<TEntity>)_dictionaryRepositories[type];
        }
    }


}
