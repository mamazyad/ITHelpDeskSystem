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

        // GET: Staff
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
                });
            }

            return View(model);
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                // Convert id to int instead of int?
                int userId = id ?? default(int);

                // find the user in the database
                var user = UserManager.FindById(userId);

                if (user != null && user is Staff)
                {
                    var staff = (Staff)user;

                    // Use Automapper instead of copying properties one by one
                    StaffViewModel model = Mapper.Map<StaffViewModel>(staff);

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

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                Staff staff = new Staff
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
                    ManagerialPosition = model.ManagerialPosition,
                };

                var result = UserManager.Create(staff, model.Password);
                db.Staffs.Add(staff);                db.SaveChanges();                if (result.Succeeded)
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

        // GET: Staff/Edit/5
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
                ManagerialPosition = staff.ManagerialPosition,
            };

            return View(model);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, StaffViewModel model)
        {
            // Exclude Password and ConfirmPassword properties from the model otherwise modelstate is false
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid && id != null)
            {

                // Convert id to non-nullable int
                var userId = id ?? default(int);

                var staff = (Staff)UserManager.FindById(userId);
                if (staff == null)
                {
                    return HttpNotFound();
                }

                // Update the properties of the v
                staff.Email = model.Email;
                staff.FirstName = model.FirstName;
                staff.LastName = model.LastName;
                staff.UserName = model.Email;
                staff.Mobile = model.Mobile;
                staff.OfficeNumber = model.OfficeNumber;
                staff.Department = model.Department;
                staff.ExtensionNumber = model.ExtensionNumber;
                staff.JobTitle = model.JobTitle;
                staff.ManagerialPosition = model.ManagerialPosition;

                var userResult = UserManager.Update(staff);

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

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var userId = id ?? default(int);
                var staff = (Staff)UserManager.FindById(userId);
                if (staff == null)
                {
                    return HttpNotFound();
                }

                StaffViewModel model = Mapper.Map<StaffViewModel>(staff);
                //model.Roles = string.Join(" ", UserManager.GetRoles(userId).ToArray());
                return View(model);
            }

            return HttpNotFound();
        }

        // POST: Staff/Delete/5
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
