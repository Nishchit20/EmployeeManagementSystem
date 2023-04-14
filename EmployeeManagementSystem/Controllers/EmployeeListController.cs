using EmployeeManagementSystem.DataAccess.Repositories.Repository.IRepository;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    /// <summary>
    /// This class is used to fetch all the user details and perform the CRUD operation. This can be accessed only for admin
    /// </summary>
    [Authorize]
    public class EmployeeListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        /// <summary>
        /// This constructor is used to access the db details
        /// </summary>
        /// <param name="db"></param>
        public EmployeeListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Here all the Employee details can be viewed. 
        /// Will have the access to change the details 
        /// Employee can be searched based on the Username,name,Email and Salary
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        // GET: EmployeeList
        public async Task<ActionResult> Index(string searchString)
        {
            IEnumerable<ApplicationUser> applicationUsers = from objEmployee in _unitOfWork.ApplicationUser.GetAll() select objEmployee;
           try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    applicationUsers = applicationUsers.Where(objEmployeeList => objEmployeeList.UserName.Contains(searchString) || objEmployeeList.Name.Contains(searchString) || objEmployeeList.Email.Contains(searchString) || objEmployeeList.Salary.ToString().Contains(searchString));
                }
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
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
        public async Task<ActionResult> Create(ApplicationUser obj)
        {
            //throw new Exception("Error in create view");
            var isEmailAlreadyExists = _unitOfWork.ApplicationUser.GetAny(obj);
            if (isEmailAlreadyExists)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
                return View(obj);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.ApplicationUser.AddAsync(obj);
                    await _unitOfWork.SaveAsync();
                    ViewBag.Message = "Successfully created";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
                }
            }
            return View(obj);
        }

        /// <summary>
        /// Here Edit GET method is performed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: EmployeeList/Edit/5
        public ActionResult Edit(string id)
        {
            
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }
                var employeeFromDbFirst = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
                if (employeeFromDbFirst == null)
                {
                    return NotFound();
                }
                return View(employeeFromDbFirst);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Here Edit POST method is performed
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser obj, string id)
        {
            var employeeFromDbFirst = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            var objEmail = obj.Email;
            if (objEmail != employeeFromDbFirst.Email)
            {
                var isEmailAlreadyExists = _unitOfWork.ApplicationUser.GetAny(obj);
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
                _unitOfWork.ApplicationUser.Update(employeeFromDbFirst);
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        await _unitOfWork.SaveAsync();
                        ViewBag.Message = "Successfully updated";
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
        /// Here Delete method is performed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            try
            {
                // Do the CRUD operation here
                _unitOfWork.ApplicationUser.Remove(obj);
                await _unitOfWork.SaveAsync();
                ViewBag.Message = "Successfully deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception here
                ViewBag.Message = $"An error occurred: {ex.Message}";
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}