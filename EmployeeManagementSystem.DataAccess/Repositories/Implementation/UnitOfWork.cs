using EmployeeManagementSystem.DataAccess.Repositories.Abstract;
using EmployeeManagementSystem.Models.Domain;
using EmployeeManagementSystem.Repositories.Abstract;

namespace EmployeeManagementSystem.DataAccess.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            //ApplicationUser = new IApplicationUserRepository(_db);
        }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IUserAuthenticationService UserAuthentication { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
