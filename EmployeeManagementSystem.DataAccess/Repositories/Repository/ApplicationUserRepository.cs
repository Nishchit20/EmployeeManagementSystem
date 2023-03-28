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

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            this.dbSet = _db.Set<ApplicationUser>();
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }

        public dynamic GetAny(ApplicationUser obj)
        {
            return _db.ApplicationUsers.Any(objEmployee => objEmployee.Email == obj.Email);
        }
        
    }
}
