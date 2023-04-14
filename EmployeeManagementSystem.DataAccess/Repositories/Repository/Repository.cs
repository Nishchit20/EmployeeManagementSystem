using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository
{
    /// <summary>
    /// This is Repository class.Here the actions required to fetch the data from database are performed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        /// <summary>
        /// The method used to add the employee to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
            }
            catch
            {
                throw new Exception("Unable to add the employee " );
            }
            return entity;
        }

        /// <summary>
        /// The method used to get all the employee details present in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            try
            {
                IQueryable<T> query = dbSet;
                return query.ToList();
            }
            catch 
            {
                throw new Exception("Unable to find all the employee ");
            }

        }

        /// <summary>
        /// The method used get the employee details based on the given id
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                IQueryable<T> query = dbSet;
                query = query.Where(filter);
                return query.FirstOrDefault();
            }
            catch 
            {
                throw new Exception("Unable to get the employee ");
            }

            
        }

        /// <summary>
        /// This method is used to performed to remove employee from the database
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            try
            {
                dbSet.Remove(entity);
            }
            catch 
            {
                throw new Exception("Unable to delete the employee ");
            }
        }


    }
}
