using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementSystem.Test
{

    public class HomeControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new HomeController(_mockUnitOfWork.Object);
        }


        /// <summary>
        /// Testing for profile update
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_Edit()
        {
            // Arrange
            string message = "Successfully updated";
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
            Assert.Equal(message, controller.ViewBag.Message);
        }
    }
}