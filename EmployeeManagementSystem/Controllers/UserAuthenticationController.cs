using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EmployeeManagementSystem.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Constructor to handle User Authentciation
        /// </summary>
        /// <param name="service">Parameter service of type type IUserAuthenticationService to inject service</param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public UserAuthenticationController(IUserAuthenticationService service, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._service = service;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Registration GET method
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Registartion POST method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "user";
            var result = await _service.RegistrationAsync(model);
            if (result.StatusCode == 1)
            {
                TempData["message"] = result.Message;
            }
            else
            {
                TempData["error"] = result.Message;
            }
            return RedirectToAction(nameof(Registration));
        }

        /// <summary>
        /// Login GET method
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login POST method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                TempData["error"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        /// <summary>
        /// Logout action is performed
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
