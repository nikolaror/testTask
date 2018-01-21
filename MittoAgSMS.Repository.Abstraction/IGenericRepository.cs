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
        Task<T> GetById(params object[] keyValues);
        Task<List<T>> GetAll(params string[] includes);
        Task<List<T>> FindBy(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
        Task<T> FirstFindBy(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        Task<int> Save();
        DbConnection Connection();
    }
}
