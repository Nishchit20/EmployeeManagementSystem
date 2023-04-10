using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.DataAccess.Services;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementSystem.Test
{
    public class EmployeeControllerTests
    {

        #region Property  
        //public Mock<IEmployeeService> mock = new Mock<IEmployeeService>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeListController _controller;
            #endregion

        public EmployeeControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new EmployeeListController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async void Test_Index()
        {
            var employeeData = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "b084dde1-13ff-4580-8df0-c34e65e4c5c8",
                    UserName = "EIU13050",
                    Name = "Nishchit",
                    Email= "nishchit@gmail.com"
                },
                new ApplicationUser
                {
                    Id = "92bc8e1e-d30e-48ac-83af-b6890a376839",
                    UserName = "EIU13001",
                    Name = "Nav",
                    Email= "nav@gmail.com"
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "EIA00001",
                    Name = "Admin",
                    Email= "adminevry22@gmail.com"
                }
            };


            _mockUnitOfWork.Setup(p => p.ApplicationUser.GetAll()).Returns(employeeData);

            // Act
            var result = await _controller.Index("Nishchit");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<ApplicationUser>>(viewResult.ViewData.Model);
            //Assert.Equal(employeeData.Count, model.Count());
            Assert.Contains(employeeData[0], data);
        }

        [Fact]
        public async void Test_Create()
        {
            var newEmployee = new ApplicationUser { UserName = "EIU13096", Name = "Nishmitha", Email = "nishmitha@gmail.com" };
            _mockUnitOfWork.Setup(p => p.ApplicationUser.GetAny(It.IsAny<ApplicationUser>())).Returns(false);
            var result = await _controller.Create(newEmployee) as RedirectToActionResult;
        }

        [Fact]
        public async Task Test_Edit()
        {
            // Arrange
            var employeeFromDbFirst = new ApplicationUser { Id = "b084dde1-13ff-4580-8df0-c34e65e4c5c8", UserName = "EIU13050", Name = "Nishchit", Email = "nishchit@gmail.com" };
            _mockUnitOfWork.Setup(uow => uow.ApplicationUser.GetFirstOrDefault(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(employeeFromDbFirst);
            //_mockEmployeeService.Setup(uow => uow.GetEmployeeById(It.IsAny<string>())).Returns(employeeFromDbFirst);
            _mockUnitOfWork.Setup(uow => uow.ApplicationUser.GetAny(It.IsAny<ApplicationUser>())).Returns(false);
            var controller = new EmployeeListController(_mockUnitOfWork.Object);
            var obj = new ApplicationUser
            {
                Id = "b084dde1-13ff-4580-8df0-c34e65e4c5c8",
                UserName = "EI13050",
                Email = "nishchitshetty@gmail.com",
                Name = "Nishchith",
                PhoneNumber = "9148847654",
                Salary = 50000
            };

            // Act
            var result = await controller.Edit(obj, "b084dde1-13ff-4580-8df0-c34e65e4c5c8") as RedirectToActionResult;

            // Assert
            _mockUnitOfWork.Verify(uow => uow.ApplicationUser.Update(employeeFromDbFirst), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
            Assert.Equal(result.ActionName, "Index");
        }

        [Fact]
        public async Task Test_DeleteValid()
        {
            // Arrange
            string id = "92bc8e1e-d30e-48ac-83af-b6890a376839";
            var user = new ApplicationUser { Id = id };
            _mockUnitOfWork.Setup(p => p.ApplicationUser.GetFirstOrDefault(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(user);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockUnitOfWork.Verify(u => u.ApplicationUser.Remove(user), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Test_DeleteInValid()
        {
            // Arrange
            string id = "92bc8e1e-d30e-48ac-83af-b6890a376839";
            _mockUnitOfWork.Setup(u => u.ApplicationUser.GetFirstOrDefault(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns((ApplicationUser)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
