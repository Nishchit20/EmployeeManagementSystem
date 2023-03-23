using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;

namespace EmployeeManagementSystem.DataAccess.Repositories.Implementation
{
    public class ApplicationUserRepository : Repository<ApplicationUser>
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
