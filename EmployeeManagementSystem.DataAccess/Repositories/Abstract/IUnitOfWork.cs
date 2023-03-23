namespace EmployeeManagementSystem.DataAccess.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }

        void Save();

    }
}
