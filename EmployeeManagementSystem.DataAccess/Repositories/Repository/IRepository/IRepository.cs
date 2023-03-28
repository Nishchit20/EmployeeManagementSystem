using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository
{
    /// <summary>
    /// This is the Interface of Repository
    /// Here T is the ApplicationUser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T,bool>> filter);
        IEnumerable<T> GetAll();
        Task<T> AddAsync(T entity);
        void Remove(T entity);
    }
}
