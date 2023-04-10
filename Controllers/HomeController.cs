using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    /// <summary>
    /// This Controller will have The home page and the User profile information
    /// </summary>
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Contructor used to handle the operation
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        /// <param name="db"></param>
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        /// <summary>
        /// Home PAGE
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }


        /// <summary>
        /// Profile view
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Privacy()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if(userId==null)
            {
                return RedirectToAction("Login", "UserAuthentication");
            }
            else
            {
                ApplicationUser user = _userManager.FindByIdAsync(userId).Result;
                return View(user);
            }
            
        }

        /// <summary>
        /// Profile update view GET method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var employeeFromDbFirst = await _db.ApplicationUsers.FindAsync(id);

            if (employeeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(employeeFromDbFirst);
        }

        /// <summary>
        /// Profile update view POST method
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser obj, string id)
        {
            var employeeFromDbFirst = await _db.ApplicationUsers.FindAsync(id);
            var objEmail = obj.Email;
            if (objEmail != employeeFromDbFirst.Email)
            {
                var isEmailAlreadyExists = await _db.ApplicationUsers.AnyAsync(objEmployee => objEmployee.Email == obj.Email);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return View(obj);
                }
            }
            employeeFromDbFirst.UserName = obj.UserName;
            employeeFromDbFirst.Email = obj.Email;
            employeeFromDbFirst.Name = obj.Name;
            employeeFromDbFirst.PhoneNumber = obj.PhoneNumber;
            employeeFromDbFirst.Salary = obj.Salary;
            if (ModelState.IsValid)
            {
                _db.ApplicationUsers.Update(employeeFromDbFirst);
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        await _db.SaveChangesAsync();                     
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {                       
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is ApplicationUser)
                            {
                                var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();
                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];                               
                                }
                                //Refresh original values to bypass next concurrency check                                
                                entry.OriginalValues.SetValues(databaseValues);
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Error View model
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
