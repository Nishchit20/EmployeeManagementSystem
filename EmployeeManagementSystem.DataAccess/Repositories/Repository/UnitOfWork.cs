using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models.Domain;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository
{
    /// <summary>
    /// This is Unit of Work class which will genric and this will have access to all the individual repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        /// <summary>
        /// This method is used to save all the details after all the changes done 
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Unable to update the employee ");
            }
        }


    }
}
