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

        //type initialized through DI container.
        public GenericRepository(DbContext entities)
        {
            _entities = entities;
        }

        public DbContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IEnumerable<T> GetAll(params string[] includes)
        {
            IQueryable<T> query = _entities.Set<T>();

            if (includes.Count() > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities.Set<T>().Where(whereCondition);

            if (includes.Any())
                return Includes(includes, query);

            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual DbConnection Connection()
        {
            return Context.Database.Connection;
        }

        //public void GetAll(PagedDataSource<T> pagedDataSource)
        //{
        //    var items = GetPagedResult(pagedDataSource);
        //    pagedDataSource.PagedData = items;
        //    pagedDataSource.TotalRecords = GetPagedResultCount(pagedDataSource);
        //}

        //public void GetAll(int currentPage, int pageSize, string sortColumnName, string sortColumnOrderType, IFormCollection collection, PagedDataSource<T> pagedDataSource)
        //{
        //    pagedDataSource.CurrentPage = currentPage;
        //    pagedDataSource.PageSize = pageSize;
        //    pagedDataSource.SortColumnName = sortColumnName;
        //    pagedDataSource.SortColumnOrderType = sortColumnOrderType;

        //    if (collection != null)
        //    {
        //        UpdateSearchFields(collection, pagedDataSource);
        //    }

        //    GetAll(pagedDataSource);
        //}

        //protected virtual int GetPagedResultCount(PagedDataSource<T> pagedDataSource)
        //{
        //    return _entities.Set<T>().Where(pagedDataSource.GetExpression()).Count();
        //}

        //protected virtual IList<T> GetPagedResult(PagedDataSource<T> pagedDataSource)
        //{
        //    IQueryable<T> result = this._entities.Set<T>();

        //    foreach (var refObj in pagedDataSource.Includes)
        //    {
        //        result = result.Include(refObj);
        //    }

        //    var orderByClause = new StringBuilder();
        //    orderByClause.Append("it.").Append(pagedDataSource.SortColumnName).Append(" ").Append(pagedDataSource.SortColumnOrderType);

        //    return result.Where(pagedDataSource.GetExpression()).
        //       OrderBy(orderByClause.ToString()).
        //       Skip((pagedDataSource.CurrentPage - 1) * pagedDataSource.PageSize).
        //       Take(pagedDataSource.PageSize).ToList();
        //}

        //protected void UpdateSearchFields(IFormCollection collection, PagedDataSource<T> dataSource)
        //{
        //    foreach (var searchField in dataSource.Columns)
        //    {
        //        if (searchField.Type == CriteriaType.DropDownListInt || searchField.Type == CriteriaType.DropDownListString)
        //        {
        //            searchField.Value = collection[searchField.KeyPropertyName];
        //        }
        //        else
        //        {
        //            searchField.Value = collection[searchField.Property];

        //            var indexFrom = new StringBuilder();
        //            indexFrom.Append(searchField.Property).Append("From");

        //            searchField.Value1 = collection[indexFrom.ToString()];

        //            var indexTo = new StringBuilder();
        //            indexTo.Append(searchField.Property).Append("To");

        //            searchField.Value2 = collection[indexTo.ToString()];
        //        }
        //    }
        //}

        public virtual T GetById(params object[] keyValues)
        {
            return _entities.Set<T>().Find(keyValues);
        }

        protected IEnumerable<T> Includes(Expression<Func<T, object>>[] includes, IQueryable<T> query)
        {
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
