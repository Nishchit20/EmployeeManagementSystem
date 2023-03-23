using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.DataAccess.Repositories.Abstract
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);
        void Save();
    }
}
