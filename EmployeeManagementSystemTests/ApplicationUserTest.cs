using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Xunit;

namespace EmployeeManagementSystemTests
{
    public class ApplicationUserTest
    {
        public void Index_Test()
        {
            //EmployeeListController obj = new EmployeeListController(null);
            //var result = obj.Index("admin", "admin@123") as ViewResult;
            //Assert.AreEqual("AdminDashboard", result.ViewName);
        }
        //#region Property  
        //public Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();  
        //#endregion  
  
        //[Fact]  
        //public async void Get()  
        //{  
        //    mock.Setup(p => p.GetAll(1)).ReturnsAsync("JK");  
        //    EmployeeListController emp = new EmployeeListController(mock.Object);  
        //    string result = await emp.GetEmployeeById(1);  
        //    Assert.Equal("JK", result);  
        //}  
        //[Fact]  
        //public async void GetEmployeeDetails()  
        //{  
        //    var employeeDTO = new ApplicationUser()  
        //    {  
        //        Name = "JK"
        //    };  
        //    mock.Setup(p => p.GetEmployeeDetails(1)).ReturnsAsync(employeeDTO);
        //    EmployeeListController emp = new EmployeeListController(mock.Object);  
        //    var result = await emp.GetEmployeeDetails(1);  
        //    Assert.True(employeeDTO.Equals(result));  
        //}  
    }
}
