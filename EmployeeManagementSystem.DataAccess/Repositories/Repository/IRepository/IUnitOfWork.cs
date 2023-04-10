using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository
{
    /// <summary>
    /// This is the Unit of Work Interface
    /// </summary>
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get;  }
        Task<int> SaveAsync();
    }
}
