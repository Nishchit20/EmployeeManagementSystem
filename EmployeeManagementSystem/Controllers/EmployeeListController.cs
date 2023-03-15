
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class EmployeeListController : Controller
    {
        private readonly ApplicationDbContext _db;

        
        public EmployeeListController(ApplicationDbContext db)
        {
            _db = db;
           
        }

        // GET: EmployeeList
       
        public ActionResult Index(string searchString)
        {
            
            IEnumerable<ApplicationUser> objEmployeeList = _db.ApplicationUsers;
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                objEmployeeList = objEmployeeList.Where(s => s.Name.Contains(searchString) || s.Email.Contains(searchString) || s.Salary.ToString().Contains(searchString));
            }
            return View(objEmployeeList);
        }

        // GET: EmployeeList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationUsers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Employee added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
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
        public  ActionResult Edit(ApplicationUser obj, string id)        
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
                                    "Don't know how to handle concurrency conflicts for "+ entry.Metadata.Name);                            
                            }                        
                        }                    
                    }                
                }            
            }            
            return RedirectToAction("Index");              
        }

        // GET: EmployeeList/Delete/5
        public ActionResult Delete(string id)
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

        // POST: EmployeeList/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(string id)
        {
            var obj = _db.ApplicationUsers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ApplicationUsers.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Employee deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
