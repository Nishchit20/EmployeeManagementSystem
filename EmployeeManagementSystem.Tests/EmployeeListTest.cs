//using EmployeeManagementSystem.Controllers;
//using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
//using EmployeeManagementSystem.Models;
//using EmployeeManagementSystem.Models.Domain;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using System;
//using System.Threading.Tasks;
//using Xunit;

//namespace EmployeeManagementSystem.Tests
//{
//    public class EmployeeListTest
//    {
//        private readonly EmployeeListController _controller;

//        public void EmployeeListController()
//        {
//            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .Options;
//            var dbContext = new ApplicationDbContext(dbContextOptions);
//            _controller = new EmployeeListController(dbContext);
//        }

//        [Fact]
//        public async Task CreateProductTest()
//        {
//            // Arrange
//            var newProduct = new ApplicationUser { Name = "Test Product"};

//            // Act
//            var result = await _controller.Edit(newProduct);

//            // Assert
//            var createdProduct = Assert.IsType<ApplicationUser>(result.Value);
//            Assert.Equal(newProduct.Name, createdProduct.Name);
//        }
//    }
//}
