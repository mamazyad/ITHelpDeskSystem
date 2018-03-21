/*
* Description: This file contains the staff controller, with the staff creation, edition, deletion, listing and details methods (actions).
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
    /// Staff controller manages the staff using Staff and StaffViewModel classes.
    /// </summary>
    public class StaffController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public StaffController()
        {
        }

        public StaffController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public object UserManeger { get; private set; }


        /// <summary>
        /// This action lists all the Staff. Staff index view is based on it.
        /// </summary>
        /// <returns>Staff, Index view</returns>
        // (GET: Staff) 
        public ActionResult Index()
        {
            var users = db.Staffs.ToList();
            var model = new List<StaffViewModel>();
            foreach (var user in users)
            {
                model.Add(new StaffViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Department = user.Department,
                    JobTitle = user.JobTitle,
                    Mobile = user.Mobile,
                    ExtensionNumber = user.ExtensionNumber,
                    OfficeNumber = user.OfficeNumber,
                    ManagerialPosition = user.ManagerialPosition,
                    StaffLevel = user.StaffLevel,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action provides the details of a specific Staff, Details view is based on it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Staff, Details vew</returns>
        // (GET: Staff/Details/5)
        public ActionResult Details(int id)
        {
            var user = UserManager.FindById(id);

            if (user != null)
            {
                var staff = (Staff)user;

                StaffViewModel model = new StaffViewModel()
                {
                    Id = staff.Id,
                    Email = staff.Email,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    UserName = staff.UserName,
                    Department = staff.Department,
                    JobTitle = staff.JobTitle,
                    Mobile = staff.Mobile,
                    ExtensionNumber = staff.ExtensionNumber,
                    OfficeNumber = staff.OfficeNumber,
                    ManagerialPosition = staff.ManagerialPosition,
                    StaffLevel = staff.StaffLevel,
                    Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
                };

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action enables the creation of a staff with role set to Staff.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Staff, Create view</returns>
        // (POST: Staff/Create) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find department
                var staff = new Staff
                {
                    ManagerialPosition = model.ManagerialPosition,
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Department = model.Department,
                    JobTitle = model.JobTitle,
                    Mobile = model.Mobile,
                    ExtensionNumber = model.ExtensionNumber,
                    OfficeNumber = model.OfficeNumber,
                    StaffLevel = model.StaffLevel,
                };

                var result = UserManager.Create(staff, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(staff.Id, "Staff");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Display error messages in the view @Html.ValidationSummary()
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }
                }
                else
                {
                    // Display error messages in the view @Html.ValidationSummary()
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    return View();
                }
            }
            return View();
        }

        // (GET: Staff/Edit/5) 
        public ActionResult Edit(int id)
        {
            var staff = (Staff)UserManager.FindById(id);
            if (staff == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            StaffViewModel model = new StaffViewModel
            {
                UserName = staff.UserName,
                Email = staff.Email,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Department = staff.Department,
                JobTitle = staff.JobTitle,
                Mobile = staff.Mobile,
                ExtensionNumber = staff.ExtensionNumber,
                OfficeNumber = staff.OfficeNumber,
                StaffLevel = staff.StaffLevel,
                ManagerialPosition = staff.ManagerialPosition,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
            };
            return View(model);
        }

        /// <summary>
        /// This action enables editing of a Staff.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Staff, Edit view</returns>
        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StaffViewModel model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var staff = (Staff)UserManager.FindById(id);
                if (staff == null)
                {
                    return HttpNotFound();
                }

                staff.Email = model.Email;
                staff.UserName = model.UserName;
                staff.FirstName = model.FirstName;
                staff.LastName = model.LastName;
                staff.Mobile = model.Mobile;
                staff.OfficeNumber = model.OfficeNumber;
                staff.Department = model.Department;
                staff.ExtensionNumber = model.ExtensionNumber;
                staff.JobTitle = model.JobTitle;

                var userResult = UserManager.Update(staff);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            var staff = (Staff)UserManager.FindById(id);

            if (staff == null)
            {
                    return HttpNotFound();
            }

            StaffViewModel model = new StaffViewModel
            {
                Id = staff.Id,
                UserName = staff.UserName,
                Email = staff.Email,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Department = staff.Department,
                JobTitle = staff.JobTitle,
                Mobile = staff.Mobile,
                ExtensionNumber = staff.ExtensionNumber,
                OfficeNumber = staff.OfficeNumber,
                StaffLevel = staff.StaffLevel,
                ManagerialPosition = staff.ManagerialPosition,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };

            return View(model);

        }


        /// <summary>
        /// This action enables the deletion of a staff.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Staff, Delete view</returns>
        // (POST: Staff/Delete/5)  
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
