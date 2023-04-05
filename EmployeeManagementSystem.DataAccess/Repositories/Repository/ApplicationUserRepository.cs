using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository
{
    /// <summary>
    /// This is the Application User Repository. This is connected to Repository where some of the common actions are taken care
    /// </summary>
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<ApplicationUser> dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            this.dbSet = _db.Set<ApplicationUser>();
        }

        /// <summary>
        /// This method is used to update the employee details
        /// </summary>
        /// <param name="obj"></param>
        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }

        /// <summary>
        /// This method is used to check whether the given data present in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public dynamic GetAny(ApplicationUser obj)
        {
            return _db.ApplicationUsers.Any(objEmployee => objEmployee.Email == obj.Email);
        }
        
    }
}
