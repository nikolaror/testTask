using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(params object[] keyValues);
        IEnumerable<T> GetAll(params string[] includes);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
        //void GetAll(PagedDataSource<T> pagedDataSource);
        //void GetAll(int currentPage, int pageSize, string sortColumnName, string sortColumnOrderType, IFormCollection collection, PagedDataSource<T> pagedDataSource);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
        DbConnection Connection();




        //IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        //IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        //T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        //void Add(params T[] items);
        //void Update(params T[] items);
        //void Remove(params T[] items);
    }
}
