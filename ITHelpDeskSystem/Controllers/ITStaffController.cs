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
    /// ITStaff controller manage the IT staff using IT Staff and ITStaffViewModel classes
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

        // GET: ITStaff
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
                    Department = user.Department,
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

        // GET: ITStaff/Details/5
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

        // POST: ITStaff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ITStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                ITStaff ITstaff = new ITStaff
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Department = model.Department,
                    JobTitle = model.JobTitle,
                    Mobile = model.Mobile,
                    ExtensionNumber = model.ExtensionNumber,
                    OfficeNumber = model.OfficeNumber,
                    Speciality = model.Speciality,
                    StartingDate = model.StartingDate,
                    Position = model.Position,
                };

                var result = UserManager.Create(ITstaff, model.Password);

                if (result.Succeeded)
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
                UserName = ITstaff.UserName,
                Email = ITstaff.Email,
                FirstName = ITstaff.FirstName,
                LastName = ITstaff.LastName,
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

        // POST: ITStaff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ITStaffViewModel model)
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
                ITstaff.Department = model.Department;
                ITstaff.ExtensionNumber = model.ExtensionNumber;
                ITstaff.JobTitle = model.JobTitle;
                ITstaff.Speciality = model.Speciality;
                ITstaff.StartingDate = model.StartingDate;
                ITstaff.Position = model.Position;

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
                UserName = ITstaff.UserName,
                Email = ITstaff.Email,
                FirstName = ITstaff.FirstName,
                LastName = ITstaff.LastName,
                Department = ITstaff.Department,
                JobTitle = ITstaff.JobTitle,
                Mobile = ITstaff.Mobile,
                ExtensionNumber = ITstaff.ExtensionNumber,
                OfficeNumber = ITstaff.OfficeNumber,
                Speciality = ITstaff.Speciality,
                StartingDate = ITstaff.StartingDate,
                Position = ITstaff.Position,
                Roles = string.Join(" ", UserManager.GetRoles(id).ToArray()),
            };

            return View(model);

        }

        // POST: ITStaff/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(id);
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
