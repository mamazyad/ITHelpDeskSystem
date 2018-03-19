/*
* Description: This file is the IT help desk admin controller, containing the admin creation, edition, deletion, listing and details methods (actions).
* Author: mamazyad
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
    /// ITHelpDesk controller manage the admin using IT Help Desk Admin and ITHelpDeskAdminViewModel classes
    /// </summary>

    public class ITHelpDeskAdminController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ITHelpDeskAdminController()
        {
        }

        public ITHelpDeskAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: ITHelpDeskAdmin
        public ActionResult Index()
        {
            var users = db.ITHelpDeskAdmins.ToList();
            var model = new List<ITHelpDeskAdminViewModel>();
            foreach (var user in users)
            {
                model.Add(new ITHelpDeskAdminViewModel
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
                    Degree = user.Degree,
                });
            }
            return View(model);
        }

        // GET: ITHelpDeskAdmin/Details/5
        public ActionResult Details(int id)
        {
            var user = UserManager.FindById(id);

            if (user != null)
            {
                var IThelpDeskAdmin = (ITHelpDeskAdmin)user;

                ITHelpDeskAdminViewModel model = new ITHelpDeskAdminViewModel()
                {
                    Id = IThelpDeskAdmin.Id,
                    Email = IThelpDeskAdmin.Email,
                    FirstName = IThelpDeskAdmin.FirstName,
                    LastName = IThelpDeskAdmin.LastName,
                    UserName = IThelpDeskAdmin.UserName,
                    Department = IThelpDeskAdmin.Department,
                    JobTitle = IThelpDeskAdmin.JobTitle,
                    Mobile = IThelpDeskAdmin.Mobile,
                    ExtensionNumber = IThelpDeskAdmin.ExtensionNumber,
                    OfficeNumber = IThelpDeskAdmin.OfficeNumber,
                    Speciality = IThelpDeskAdmin.Speciality,
                    StartingDate = IThelpDeskAdmin.StartingDate,
                    Position = IThelpDeskAdmin.Position,
                    Degree = IThelpDeskAdmin.Degree,
                    Roles = string.Join(" ", UserManager.GetRoles(id).ToArray())
                };

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: ITHelpDeskAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ITHelpDeskAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ITHelpDeskAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var IThelpDeskAdmin = new ITHelpDeskAdmin
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
                    Degree = model.Degree,
                };

                var result = UserManager.Create(IThelpDeskAdmin, model.Password);

                if (result.Succeeded)
                {
                    var roleResult = UserManager.AddToRoles(IThelpDeskAdmin.Id, "Admin");

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


        // GET: ITHelpDeskAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            var IThelpDeskAdmin = (ITHelpDeskAdmin)UserManager.FindById(id);
            if (IThelpDeskAdmin == null)
            {
                //return HttpNotFound();
                return View("Error");
            }

            ITHelpDeskAdminViewModel model = new ITHelpDeskAdminViewModel
            {
                Id = IThelpDeskAdmin.Id,
                Email = IThelpDeskAdmin.Email,
                UserName = IThelpDeskAdmin.UserName,
                FirstName = IThelpDeskAdmin.FirstName,
                LastName = IThelpDeskAdmin.LastName,
                Mobile = IThelpDeskAdmin.Mobile,
                OfficeNumber = IThelpDeskAdmin.OfficeNumber,
                Department = IThelpDeskAdmin.Department,
                ExtensionNumber = IThelpDeskAdmin.ExtensionNumber,
                JobTitle = IThelpDeskAdmin.JobTitle,
                Speciality = IThelpDeskAdmin.Speciality,
                StartingDate = IThelpDeskAdmin.StartingDate,
                Position = IThelpDeskAdmin.Position,
                Degree = IThelpDeskAdmin.Degree,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };
            return View(model);
        }

        // POST: ITHelpDeskAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ITHelpDeskAdminViewModel model, params string[] roles)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var IThelpDeskAdmin = (ITHelpDeskAdmin)UserManager.FindById(id);
                if (IThelpDeskAdmin == null)
                {
                    return HttpNotFound();
                }

                IThelpDeskAdmin.Email = model.Email;
                IThelpDeskAdmin.UserName = model.UserName;
                IThelpDeskAdmin.FirstName = model.FirstName;
                IThelpDeskAdmin.LastName = model.LastName;
                IThelpDeskAdmin.Mobile = model.Mobile;
                IThelpDeskAdmin.OfficeNumber = model.OfficeNumber;
                IThelpDeskAdmin.ExtensionNumber = model.ExtensionNumber;
                IThelpDeskAdmin.JobTitle = model.JobTitle;
                IThelpDeskAdmin.Speciality = model.Speciality;
                IThelpDeskAdmin.StartingDate = model.StartingDate;
                IThelpDeskAdmin.Position = model.Position;
                IThelpDeskAdmin.Degree = model.Degree;

                var userResult = UserManager.Update(IThelpDeskAdmin);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: ITHelpDeskAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var IThelpDeskAdmin = (ITHelpDeskAdmin)UserManager.FindById(id);
            if (IThelpDeskAdmin == null)
            {
                return HttpNotFound();
            }

            ITHelpDeskAdminViewModel model = new ITHelpDeskAdminViewModel
            {
                Id = IThelpDeskAdmin.Id,
                Email = IThelpDeskAdmin.Email,
                UserName = IThelpDeskAdmin.UserName,
                FirstName = IThelpDeskAdmin.FirstName,
                LastName = IThelpDeskAdmin.LastName,
                Mobile = IThelpDeskAdmin.Mobile,
                OfficeNumber = IThelpDeskAdmin.OfficeNumber,
                Department = IThelpDeskAdmin.Department,
                ExtensionNumber = IThelpDeskAdmin.ExtensionNumber,
                JobTitle = IThelpDeskAdmin.JobTitle,
                Speciality = IThelpDeskAdmin.Speciality,
                StartingDate = IThelpDeskAdmin.StartingDate,
                Position = IThelpDeskAdmin.Position,
                Degree = IThelpDeskAdmin.Degree,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };

            return View(model);

        }

        // POST: ITHelpDeskAdmin/Delete/5
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
