using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Services
{
    public interface IEmployeeService
    {
        IEnumerable<ApplicationUser> GetAllEmployees();
        ApplicationUser GetEmployeeById(string id);
        void CreateEmployee(EmployeeViewModel employeeViewModel);
        void UpdateEmployee(EmployeeViewModel employeeViewModel);
        void DeleteEmployee(EmployeeViewModel employeeViewModel);
    }
}
