using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository
{
    /// <summary>
    /// This is the Interface of ApplicationUserRepository.
    /// </summary>
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);

        dynamic GetAny(ApplicationUser obj);
    }
}
