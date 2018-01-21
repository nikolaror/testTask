using MittoAgSMS.DataAccessLayer.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer
{
    public abstract class GenericRepository<T> :
     IGenericRepository<T> where T : class
    {
        private DbContext _entities;

        public GenericRepository(DbContext entities)
        {
            _entities = entities;
        }

        public DbContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual async Task<List<T>> GetAll(params string[] includes)
        {
            IQueryable<T> query = _entities.Set<T>();

            if (includes.Count() > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> FindBy(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities.Set<T>().Where(whereCondition);

            if (includes.Any())
                return await Includes(includes, query);

            return await query.ToListAsync();
        }

        public async Task<T> FirstFindBy(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities.Set<T>().Where(whereCondition);

            if (includes.Any())
                return await Includes(includes, query).Result.AsQueryable().FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }


        public virtual async Task<int> Save()
        {
            return await _entities.SaveChangesAsync();
        }

        public virtual DbConnection Connection()
        {
            return Context.Database.Connection;
        }
        
        public virtual async Task<T> GetById(params object[] keyValues)
        {
            return await _entities.Set<T>().FindAsync(keyValues);
        }

        protected async Task<List<T>> Includes(Expression<Func<T, object>>[] includes, IQueryable<T> query)
        {
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }
    }
}
