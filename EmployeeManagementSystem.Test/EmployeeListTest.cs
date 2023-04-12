////using EmployeeManagementSystem.DataAccess.Repositories.Repository;
////using EmployeeManagementSystem.Models;
////using EmployeeManagementSystem.Models.Domain;
////using Microsoft.EntityFrameworkCore;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Xunit;

////namespace EmployeeManagementSystem.Test
////{
////    public class EmployeeListTest
////    {
       
////        public static string connectionString = "Server=ELW5146;Database=EmpLoyeeMS;Trusted_Connection=True;TrustServerCertificate=True";

////        /// <summary>
////        /// GetFirstorDefault test case
////        /// Add new entry in database 
////        /// verify data present in db using GetFirstOrDefault
////        /// Cleanup the newly added data
////        /// </summary>
////        /// <returns></returns>
////        [Fact]
////        public async Task Test_EmployeeList_GetFirstorDefault()
////        {
////            // Arrange
////            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
////                .UseSqlServer(connectionString)
////                .Options;

////            using (var context = new ApplicationDbContext(options))
////            {
////                //Adding data to database
////                var user = new ApplicationUser { Name = "Nish", UserName = "EIU120058", Email = "shettynish123@gmail.com" };
////                context.ApplicationUsers.Add(user);
////                context.SaveChanges();

////                var userRepository = new Repository<ApplicationUser>(context);

////                // Act
////                var retrievedUser = userRepository.GetFirstOrDefault(u=>u.Name == "Nish");

////                //Assert
////                Assert.NotNull(retrievedUser);
////                Assert.Equal("Nish", retrievedUser.Name);

////                //Data Cleanup
////                context.ApplicationUsers.Remove(retrievedUser);
////                context.SaveChanges();
////            }   
////        }

////        /// <summary>
////        /// Assume the data entries in database is 5
////        /// verify count matches and check whether it is null
////        /// Give values to the expected user
////        /// Check whether the data present in expected data and retrived data matches
////        /// usecompare is used check whether the data is equal or not
////        /// </summary>
////        [Fact]
////        public void Test_EmployeeList_GetAll()
////        {
////            // Arrange
////            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
////                .UseSqlServer(connectionString)
////                .Options;

////            using (var context = new ApplicationDbContext(options))
////            {
////                var userRepository = new Repository<ApplicationUser>(context);

////                // Act
////                var retrievedUsers = userRepository.GetAll();

////                // Assert
////                Assert.NotNull(retrievedUsers);
////                Assert.Equal(5, retrievedUsers.Count());

////                var expectedUsers = new List<ApplicationUser>
////                {
////                    new ApplicationUser { Name = "mav", UserName = "EIU13002", Email = "mav@gmail.com" },
////                    new ApplicationUser { Name = "rav", UserName = "EIU13003", Email = "rav@gmail.com" },
////                    new ApplicationUser { Name = "Admin", UserName = "EIA00001", Email = "adminevry22@gmail.com" },
////                    new ApplicationUser { Name = "Nav", UserName = "EIU13001", Email = "nav@gmail.com" },
////                    new ApplicationUser { Name = "Nishchit", UserName = "EIU13050", Email = "nishchit@gmail.com" }

////                };

////                Assert.Equal(expectedUsers, retrievedUsers, new UserComparer());
////            }
////        }

////        private class UserComparer : IEqualityComparer<ApplicationUser>
////        {
////            public bool Equals(ApplicationUser x, ApplicationUser y)
////            {
////                return x.UserName == y.UserName &&
////                    x.Name == y.Name &&
////                    x.Email == y.Email;
////            }

////            public int GetHashCode(ApplicationUser obj)
////            {
////                return obj.Name.GetHashCode();
////            }
////        }
////    }
////}
