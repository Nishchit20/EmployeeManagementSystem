using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.DataAccess.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        //T->Employee
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
}
