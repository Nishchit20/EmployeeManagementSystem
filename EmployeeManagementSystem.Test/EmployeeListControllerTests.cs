using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementSystem.Test
{
    public class EmployeeListControllerTests
    {
        #region Property  
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly EmployeeListController _controller;
            #endregion

        public EmployeeListControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new EmployeeListController(_mockUnitOfWork.Object);
        }

        /// <summary>
        /// Testing of Index page
        /// </summary>
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
            Assert.Contains(employeeData[0], data);
        }

        /// <summary>
        /// Testing create method
        /// </summary>
        [Fact]
        public async void Test_Create()
        {
            //Arrange
            string message = "Successfully created";
            var newEmployee = new ApplicationUser { UserName = "EIU13096", Name = "Nishmitha", Email = "nishmitha@gmail.com" };
            _mockUnitOfWork.Setup(p => p.ApplicationUser.GetAny(It.IsAny<ApplicationUser>())).Returns(false);

            //Act
            var result = await _controller.Create(newEmployee) as RedirectToActionResult;

            //Assert
            Assert.Equal(message, _controller.ViewBag.Message);
        }

        /// <summary>
        /// Testing Edit method
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_Edit()
        {
            // Arrange
            string message = "Successfully updated";
            var employeeFromDbFirst = new ApplicationUser { Id = "b084dde1-13ff-4580-8df0-c34e65e4c5c8", UserName = "EIU13050", Name = "Nishchit", Email = "nishchit@gmail.com" };
            _mockUnitOfWork.Setup(uow => uow.ApplicationUser.GetFirstOrDefault(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(employeeFromDbFirst);
            _mockUnitOfWork.Setup(uow => uow.ApplicationUser.GetAny(It.IsAny<ApplicationUser>())).Returns(false);
            
            var obj = new ApplicationUser
            {
                Id = "b084dde1-13ff-4580-8df0-c34e65e4c5c8",
                UserName = "EI13050",
                Email = "nishchitshetty@gmail.com",
                Name = "Nishchith",
                PhoneNumber = "9148847654",
                Salary = 60000
            };

            // Act
            var result = await _controller.Edit(obj, "b084dde1-13ff-4580-8df0-c34e65e4c5c8") as RedirectToActionResult;

            // Assert
            _mockUnitOfWork.Verify(uow => uow.ApplicationUser.Update(employeeFromDbFirst), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
            Assert.Equal(message, _controller.ViewBag.Message);
        }

        /// <summary>
        /// Testing Delete method
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_Delete()
        {
            // Arrange
            string id = "92bc8e1e-d30e-48ac-83af-b6890a376839";
            string message = "Successfully deleted";
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
            Assert.Equal(message, _controller.ViewBag.Message);
        }
    }
}