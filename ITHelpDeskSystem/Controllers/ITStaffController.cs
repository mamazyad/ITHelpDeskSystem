/*Description: This file cntains the IT staff controller, with the IT staff edition, deletion, listing and details methods (actions). In adition, the creation method enables the creation of an IT staff with the role IT Manager.
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
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ITHelpDeskSystem.Controllers
{
    /// <summary>
    /// ITStaff controller manages the IT staff using IT Staff and ITStaffViewModel classes.
    /// </summary>

    public class ITStaffController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ITStaffController()
        {
        }

        public ITStaffController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        /// This action lists all the IT staff. IT staff index view is based on it.
        /// </summary>
        /// <returns>IT staff, Index view</returns>
        // (GET: ITStaff) 
        public ActionResult Index()
        {
            var users = db.ITStaffs.ToList();
            var model = new List<ITStaffViewModel>();
            foreach (var user in users)
            {
                model.Add(new ITStaffViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    JobTitle = user.JobTitle,
                    Mobile = user.Mobile,
                    ExtensionNumber = user.ExtensionNumber,
                    OfficeNumber = user.OfficeNumber,
                    Speciality = user.Speciality,
                    StartingDate = user.StartingDate,
                    Position = user.Position,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action provides the details of a specific IT staff, Details view is based on it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ITStaff, Details view</returns>
        // (GET: ITStaff/Details/5)
        public ActionResult Details(int id)
        {
            var user = UserManager.FindById(id);

            if (user != null)
            {
                var ITstaff = (ITStaff)user;

                ITStaffViewModel model = new ITStaffViewModel()
                {
                    Id = ITstaff.Id,
                    Email = ITstaff.Email,
                    FirstName = ITstaff.FirstName,
                    LastName = ITstaff.LastName,
                    UserName = ITstaff.UserName,
                    Department = ITstaff.Department,
                    JobTitle = ITstaff.JobTitle,
                    Mobile = ITstaff.Mobile,
                    ExtensionNumber = ITstaff.ExtensionNumber,
                    OfficeNumber = ITstaff.OfficeNumber,
                    Speciality = ITstaff.Speciality,
                    StartingDate = ITstaff.StartingDate,
                    Position = ITstaff.Position,
                    Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
                };

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: ITStaff/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action enables the creation of an IT staff with role set to ITStaff and IT manager with role IT manager.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ITStaff, create view</returns>
        // (POST: ITStaff/Create) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ITStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ITstaff = new ITStaff
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Department = "IT Department",
                    JobTitle = model.JobTitle,
                    Mobile = model.Mobile,
                    ExtensionNumber = model.ExtensionNumber,
                    OfficeNumber = model.OfficeNumber,
                    Speciality = model.Speciality,
                    StartingDate = model.StartingDate,
                    Position = model.Position,
                    IsManager = model.IsManager,
                };

                var result = UserManager.Create(ITstaff, model.Password);

                if (result.Succeeded && model.IsManager==true)
                {
                    var roleResult = UserManager.AddToRoles(ITstaff.Id, "ITManager");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }
                }
                if (result.Succeeded && model.IsManager == false)
                {
                    var roleResult = UserManager.AddToRoles(ITstaff.Id, "ITStaff");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, roleResult.Errors.First());
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.First());
                    return View();
                }
            }

            return View();

        }

        // GET: ITStaff/Edit/5
        public ActionResult Edit(int id)
        {
            var ITstaff = (ITStaff)UserManager.FindById(id);
            if (ITstaff == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            ITStaffViewModel model = new ITStaffViewModel
            {
                Id = ITstaff.Id,
                Email = ITstaff.Email,
                UserName = ITstaff.UserName,
                FirstName = ITstaff.FirstName,
                LastName = ITstaff.LastName,
                Mobile = ITstaff.Mobile,
                OfficeNumber = ITstaff.OfficeNumber,
                Department = ITstaff.Department,
                ExtensionNumber = ITstaff.ExtensionNumber,
                JobTitle = ITstaff.JobTitle,
                Speciality = ITstaff.Speciality,
                StartingDate = ITstaff.StartingDate,
                Position = ITstaff.Position,
                IsManager = ITstaff.IsManager,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };
            return View(model);
        }

        /// <summary>
        /// This action enables editing of an IT Staff.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="roles"></param>
        /// <returns>ITStaff, Edit view</returns>
        // (POST: ITStaff/Edit/5) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ITStaffViewModel model, params string[] roles)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var ITstaff = (ITStaff)UserManager.FindById(id);
                if (ITstaff == null)
                {
                    return HttpNotFound();
                }

                ITstaff.Email = model.Email;
                ITstaff.UserName = model.UserName;
                ITstaff.FirstName = model.FirstName;
                ITstaff.LastName = model.LastName;
                ITstaff.Mobile = model.Mobile;
                ITstaff.OfficeNumber = model.OfficeNumber;
                ITstaff.ExtensionNumber = model.ExtensionNumber;
                ITstaff.JobTitle = model.JobTitle;
                ITstaff.Speciality = model.Speciality;
                ITstaff.StartingDate = model.StartingDate;
                ITstaff.Position = model.Position;
                ITstaff.IsManager = model.IsManager;

                var userResult = UserManager.Update(ITstaff);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            return View();
        }

        // GET: ITStaff/Delete/5
        public ActionResult Delete(int id)
        {
            var ITstaff = (ITStaff)UserManager.FindById(id);
            if (ITstaff == null)
            {
                return HttpNotFound();
            }

            ITStaffViewModel model = new ITStaffViewModel
            {
                Id = ITstaff.Id,
                Email = ITstaff.Email,
                UserName = ITstaff.UserName,
                FirstName = ITstaff.FirstName,
                LastName = ITstaff.LastName,
                Mobile = ITstaff.Mobile,
                OfficeNumber = ITstaff.OfficeNumber,
                Department = ITstaff.Department,
                ExtensionNumber = ITstaff.ExtensionNumber,
                JobTitle = ITstaff.JobTitle,
                Speciality = ITstaff.Speciality,
                StartingDate = ITstaff.StartingDate,
                Position = ITstaff.Position,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };

            return View(model);

        }

        /// <summary>
        ///  This action enables the deletion of an IT staff.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ITStaff, Delete view</returns>
        // (POST: ITStaff/Delete/5)
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
