/*
* Description: This file contains the employees controller, with the employee creation, edition, deletion, listing and details methods (actions).
* Author: mamazyad
* Date: 20/03/2018
*/

using AutoMapper;
using ITHelpDeskSystem.Models;
using ITHelpDeskSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITHelpDeskSystem.Controllers
{

    /// <summary>
    /// Employee controller manages the employees using Employee and EmployeeViewModel classes.
    /// </summary>
    public class EmployeeController : Controller
    {
        
            private ApplicationSignInManager _signInManager;
            private ApplicationUserManager _userManager;
            private ApplicationDbContext db = new ApplicationDbContext();

            public EmployeeController()
            {
            }

            public EmployeeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            {
                UserManager = userManager;
                SignInManager = signInManager;
            }

            public ApplicationSignInManager SignInManager
            {
                get
                {
                    return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }
                private set
                {
                    _signInManager = value;
                }
            }

            public ApplicationUserManager UserManager
            {
                get
                {
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                private set
                {
                    _userManager = value;
                }
            }

        /// <summary>
        /// This action lists all the employees. Employee index view is based on it.
        /// </summary>
        /// <returns>Employee, Index view</returns>
        // (GET: Employee)  
        public ActionResult Index()
        {
            var users = db.Employees.ToList();
            var model = new List<EmployeeViewModel>();

            foreach (var item in users)
            {
                    model.Add(new EmployeeViewModel
                    {
                        Id = item.Id,
                        Email = item.Email,
                        UserName = item.UserName,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Department = item.Department,
                        JobTitle = item.JobTitle,
                        Mobile = item.Mobile,
                        ExtensionNumber = item.ExtensionNumber,
                        OfficeNumber = item.OfficeNumber,
                    });
            }

            return View(model);
        }

        /// <summary>
        /// This action provides the details of a specific employee. Employee details view is based on it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee, Details view</returns>
        // (GET: Employee/Details/5) 
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                int userId = id ?? default(int);

                var user = UserManager.FindById(userId);

                if (user != null && user is Employee)
                {
                    var employee = (Employee)user;

                    EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                   model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());

                    return View(model);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Employee/Create. 
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action enables the creation of an employee. Employee create view is based on it.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Employee, create model</returns>
        // (POST: Employee/Create) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Department = model.Department,
                    JobTitle = model.JobTitle,
                    Mobile = model.Mobile,
                    ExtensionNumber = model.ExtensionNumber,
                    OfficeNumber = model.OfficeNumber,
                };

                var result = UserManager.Create(employee, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }

            {
                return View();
            }
        }


        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {

            var employee = (Employee)UserManager.FindById(id);
            if (employee == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            EmployeeViewModel model = new EmployeeViewModel
            {
                UserName = employee.UserName,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Department = employee.Department,
                JobTitle = employee.JobTitle,
                Mobile = employee.Mobile,
                ExtensionNumber = employee.ExtensionNumber,
                OfficeNumber = employee.OfficeNumber,
            };
            return View(model);
        }


        /// <summary>
        /// This action enables editing of an employee. Employee edit view is based on it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="roles"></param>
        /// <returns>Employee, edit view</returns>
        // (POST: Employee/Edit/5) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, EmployeeViewModel model, params string[] roles)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {

                // Convert id to non-nullable int
                var userId = id ?? default(int);

                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                employee.Email = model.Email;
                employee.UserName = model.UserName;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Mobile = model.Mobile;
                employee.OfficeNumber = model.OfficeNumber;
                employee.Department = model.Department;
                employee.ExtensionNumber = model.ExtensionNumber;
                employee.JobTitle = model.JobTitle;
                
                var userResult = UserManager.Update(employee);

                if (userResult.Succeeded)
                {
                    var userRoles = UserManager.GetRoles(employee.Id);
                    roles = roles ?? new string[] { };
                    var roleResult = UserManager.AddToRoles(employee.Id, roles.Except(userRoles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    roleResult = UserManager.RemoveFromRoles(employee.Id, userRoles.Except(roles).ToArray<string>());

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);
                var employee = (Employee)UserManager.FindById(userId);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);
                //model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());
                return View(model);
            }

            return HttpNotFound();
        }


        /// <summary>
        /// This action enables the deletion of an employee. Employee delete view is based on it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee, Delete view</returns>
        // (POST: Employee/Delete/5) 
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {
                var userId = id ?? default(int);
                var user = UserManager.FindById(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}
