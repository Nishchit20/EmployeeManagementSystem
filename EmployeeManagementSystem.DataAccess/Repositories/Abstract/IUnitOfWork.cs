using EmployeeManagementSystem.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagementSystem.DataAccess.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }

        void Save();

    }
}
