using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Threading.Tasks;

using Xunit;

namespace EmployeeManagementSystem.Test
{
    public class UserAuthenticationControllerTest
    {
        private readonly Mock<IUserAuthenticationService> _mockUserAuthService;
        private readonly UserAuthenticationController _controller;
        

        public UserAuthenticationControllerTest()
        {
            _mockUserAuthService = new Mock<IUserAuthenticationService>();
            _controller = new UserAuthenticationController(_mockUserAuthService.Object);
        }
     

        /// <summary>
        /// Testing for Registration
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_Registration()
        {
            // Arrange
            var testModel = new RegistrationModel { Email = "test@example.com", Password = "Password@123" };
            _mockUserAuthService.Setup(x => x.RegistrationAsync(testModel))
                       .ReturnsAsync(new Status { StatusCode = 1, Message = "Registration successful." });
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            // Act
            var result = await _controller.Registration(testModel) as RedirectToActionResult;

            // Assert
            _mockUserAuthService.Verify(x => x.RegistrationAsync(testModel), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Registration", result.ActionName);
            Assert.Null(result.ControllerName);
            Assert.Equal("Registration successful.", _controller.TempData["message"]);
        }

        /// <summary>
        /// Testing for Login
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_Login()
        {
            // Arrange
            var testModel = new LoginModel { Username = "EIU13059", Password = "password123" };

            _mockUserAuthService.Setup(x => x.LoginAsync(testModel))
                       .ReturnsAsync(new Status { StatusCode = 1, Message = "Logged in Successfully" });

            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            // Act
            var result = await _controller.Login(testModel) as RedirectToActionResult;

            // Assert
            _mockUserAuthService.Verify(x => x.LoginAsync(testModel), Times.Once);
            Assert.NotNull(result);
            //Assert.Equal("Index", result.ActionName);
            //Assert.Equal("Home", result.ControllerName);
            Assert.Equal("Logged in Successfully", _controller.TempData["message"]);
        }

        /// <summary>
        /// Testing for Logout
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_Logout()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = await _controller.Logout() as RedirectToActionResult;

            // Assert
            _mockUserAuthService.Verify(x => x.LogoutAsync(), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(nameof(UserAuthenticationController.Login), result.ActionName);
        }
    }
}