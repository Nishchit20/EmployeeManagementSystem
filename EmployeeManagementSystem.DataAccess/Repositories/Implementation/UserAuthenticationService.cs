using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repositories.Implementation
{
    /// <summary>
    /// This is UserAuthenticationService repository which will handle all the operation Which are performed during the Login and Registration
    /// </summary>
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Contructor for user authentication
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// Enum values
        /// </summary>
        enum Value
        {
            Low,
            High
        }

        /// <summary>
        /// Here the various methods are described which will be performed during Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            
        var user = await userManager.FindByNameAsync(model.Username);
            if(user == null)
            {
                status.StatusCode = (int)Value.Low;
                status.Message = "Invalid Employee-ID";
                return status;
            }

            //we will match password
            if(!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = (int)Value.Low;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if(signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = (int)Value.High;
                status.Message = "Logged in Successfully";
                return status;
            }
            else if(signInResult.IsLockedOut)
            {
                status.StatusCode = (int)Value.Low;
                status.Message = "User locked out";
                return status;
            }
            else
            {
                status.StatusCode = (int)Value.Low;
                status.Message = "Error on logging in";
                return status;
            }
        }

        /// <summary>
        /// This is the Logout method
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        /// <summary>
        /// The various methods performed during the User Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            var status = new Status();

            var EmailExists = await userManager.FindByEmailAsync(model.Email);
            if (EmailExists != null)
            {
                status.StatusCode = (int)Value.Low;
                status.Message = "User already exists";
                return status;
            }
            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                status.StatusCode = (int)Value.Low;
                status.Message = "User creation failed";
                return status;
            }

            //Roll management
            if (!await roleManager.RoleExistsAsync(model.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = (int)Value.High;
            status.Message = "User has registered successfully";
            return status;
        }
    }
}
