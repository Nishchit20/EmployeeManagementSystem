using EmployeeManagementSystem.DataAccess.Repositories.Abstract;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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

        // GET: EmployeeList/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "" || id == null)
            {
                return NotFound();
            }

            var employeeFromDbFirst = _db.ApplicationUsers.Find(id);

            if (employeeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(employeeFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser obj, string id)
        {
            var employeeFromDbFirst = _db.ApplicationUsers.Find(id);
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
                        _db.SaveChanges();
                        //TempData["success"] = "Cannot update                         
                        //return RedirectToAction("Index");                        
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        // return RedirectToAction("Index");                        
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

                                    //TODO: decide which value should be written to database                                    
                                    //if(property.Name == "Salary")                                    
                                    //  proposedValues[property] =proposedValue;                                
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
