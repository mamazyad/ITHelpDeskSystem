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
    
            // GET: Employee
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

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                // Convert id to int instead of int?
                int userId = id ?? default(int);

                // find the user in the database
                var user = UserManager.FindById(userId);

                // Check if the user exists and it is an emplyee not a simple application user
                if (user != null && user is Employee)
                {
                    var employee = (Employee)user;

                    // Use Automapper instead of copying properties one by one
                    EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

                    //model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());

                    return View(model);
                }
                else
                {
                    // Customize the error view: /Views/Shared/Error.cshtml
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
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
                Id = employee.Id,
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

            

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, EmployeeViewModel model)
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

                // Update the properties of the employee
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
                    //var userRoles = UserManager.GetRoles(employee.Id);
                   // roles = roles ?? new string[] { };
                   // var roleResult = UserManager.AddToRoles(employee.Id, roles.Except(userRoles).ToArray<string>());

                    //if (!roleResult.Succeeded)
                    //{
                    //    ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                    //    return View();
                    //}

                    //roleResult = UserManager.RemoveFromRoles(employee.Id, userRoles.Except(roles).ToArray<string>());

                    //if (!roleResult.Succeeded)
                    //{
                    //    ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                    //    return View();
                    //}

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

        // POST: Employee/Delete/5
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
