using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models.Domain;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository
{
    /// <summary>
    /// This is Unit of Work class which will genric and this will have access to all the individual repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
