using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{
    /// <summary>
    /// This class is used to fetch all the user details and perform the CRUD operation. This can be accessed only for admin
    /// </summary>
    [Authorize]
    public class EmployeeListController : Controller
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// This constructor is used to access the db details
        /// </summary>
        /// <param name="db"></param>
        public EmployeeListController(ApplicationDbContext db)
        {
            _db = db;  
        }

        /// <summary>
        /// Here all the Employee details can be viewed. 
        /// Will have the access to change the details 
        /// Employee can be searched based on the Username,name,Email and Salary
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        // GET: EmployeeList
        public ActionResult Index(string searchString)
        {
            var applicationUsers = from objEmployee in _db.ApplicationUsers select objEmployee;
            if (!String.IsNullOrEmpty(searchString))
            {
                applicationUsers = applicationUsers.Where(objEmployeeList => objEmployeeList.UserName.Contains(searchString) || objEmployeeList.Name.Contains(searchString) || objEmployeeList.Email.Contains(searchString) || objEmployeeList.Salary.ToString().Contains(searchString));
            }
            return View(applicationUsers.ToList());
        }

        /// <summary>
        /// Here Create GET method will be performed
        /// </summary>
        /// <returns></returns>
        // GET: EmployeeList/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Here Create POST method will be performed
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: EmployeeList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser obj)
        {
           
            var isEmailAlreadyExists = _db.ApplicationUsers.Any(objEmployee => objEmployee.Email == obj.Email);
            if (isEmailAlreadyExists)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
                return View(obj);
            }

            if (ModelState.IsValid)
            {
                _db.ApplicationUsers.Add(obj);
                _db.SaveChanges();
                //TempData["success"] = "Employee added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /// <summary>
        /// Here Edit GET method is performed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: EmployeeList/Edit/5
        public IActionResult Edit(string id)
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

        /// <summary>
        /// Here Edit POST method is performed
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser obj, string id)        
        {
            var employeeFromDbFirst = _db.ApplicationUsers.Find(id);
            var objEmail = obj.Email;
            if(objEmail!=employeeFromDbFirst.Email)
            {
                var isEmailAlreadyExists = _db.ApplicationUsers.Any(objEmployee => objEmployee.Email == obj.Email);
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
                        _db.SaveChanges();                                              
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
                                    "Don't know how to handle concurrency conflicts for "+ entry.Metadata.Name);                            
                            }                        
                        }                    
                    }                
                }            
            }            
            return RedirectToAction("Index");              
        }

        /// <summary>
        /// Here Delete method is performed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var obj = _db.ApplicationUsers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ApplicationUsers.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
