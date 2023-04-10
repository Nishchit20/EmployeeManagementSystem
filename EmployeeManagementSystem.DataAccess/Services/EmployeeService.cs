using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementSystem.DataAccess.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ApplicationUser> _employees;

        public EmployeeService()
        {
            _employees = new List<ApplicationUser>
        {
            new ApplicationUser { Name = "John Doe",  Email = "johndoe@example.com", PhoneNumber = "123-456-7890" },
            new ApplicationUser { Name = "Jane Smith",  Email = "janesmith@example.com", PhoneNumber = "555-555-5555" }
        };
        }

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<ApplicationUser> GetAllEmployees()
        {
            return _unitOfWork.ApplicationUser.GetAll();
        }

        public ApplicationUser GetEmployeeById(string id)
        {
            return _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
        }

        public void CreateEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new ApplicationUser
            {
                UserName = employeeViewModel.UserName,
                Name = employeeViewModel.Name,
                Email = employeeViewModel.Email
            };
            _unitOfWork.ApplicationUser.AddAsync(employee);
        }

        public void UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new ApplicationUser
            {
                UserName = employeeViewModel.UserName,
                Name = employeeViewModel.Name,
                Email = employeeViewModel.Email
            };
            _unitOfWork.ApplicationUser.Update(employee);
        }

        public void DeleteEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new ApplicationUser
            {
                UserName = employeeViewModel.UserName,
                Name = employeeViewModel.Name,
                Email = employeeViewModel.Email
            };
            _unitOfWork.ApplicationUser.Remove(employee);
        }

    }
}
