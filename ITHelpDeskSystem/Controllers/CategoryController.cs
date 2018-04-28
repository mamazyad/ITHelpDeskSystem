/*
* Description: This file contains the category controller with the category creation, edition, deletion, listing and details methods (actions).
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
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all the categories created with the IT staff responsible for them. Category index view is based on it.
        /// </summary>
        /// <returns> Category, Index view</returns>
        // GET: Category 
        [Authorize(Roles = "ITStaff, Admin")]
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            var model = new List<CategoryViewModel>();
            foreach (var item in categories)
            {
                model.Add(new CategoryViewModel
                {
                    Id = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryDescription = item.CategoryDescription,
                    ITStaff = item.ITStaff.FullName,
                });
            }
            return View(model);
        }

        /// <summary>
        ///  This action provides the details of a specific category, Category Details view is based on it with links to Delete and Edit actions.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category, Details view</returns>
        // GET: Category/Details/5
        [Authorize(Roles = "ITStaff, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var model = new CategoryViewModel
            {
                Id = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ITStaff = category.ITStaff.FullName,
            };

            return View(model);
        }

        /// <summary>
        /// This action enables the creation of a category (with a unique name) and assigning it to a specific IT staff.
        /// </summary>
        /// <param name="model">Ticket model</param>
        /// <returns> Category, Create view</returns>
        // GET: Category/Create. 
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var list = db.ITStaffs.Where(m => m.IsManager == false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the creation of a category (with a unique name) and assigning it to a specific IT staff.
        /// </summary>
        /// <param name="model">Ticket model</param>
        /// <returns> Category, Create view</returns>
        // (POST: Category/Create) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryId = model.Id,
                    CategoryName = model.CategoryName,
                    CategoryDescription = model.CategoryDescription,
                    ITStaffId = model.ITStaffId,
                };

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var list = db.ITStaffs.Where(m=>m.IsManager==false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a category.
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <param name="model">Ticket model</param>
        /// <returns> Category, Edit view</returns>
        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            CategoryViewModel model = new CategoryViewModel
            {
                Id = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ITStaffId = category.ITStaffId,
            };
            var list = db.ITStaffs.Where(m => m.IsManager == false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");
            return View(model);
        }

        /// <summary>
        /// This action enables the editing of a category.
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <param name="model">Ticket model</param>
        /// <returns> Category, Edit view</returns>
        // (POST: Category/Edit/5) 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                category.CategoryName = model.CategoryName;
                category.CategoryDescription = model.CategoryDescription;
                category.ITStaffId = model.ITStaffId;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var list = db.ITStaffs.Where(m => m.IsManager == false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");
            return View(model);
        }

        /// <summary>
        /// This action enables the deletion of a category.
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <returns> Category, Delete view</returns>
        // GET: Category/Delete/5.
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var model = new CategoryViewModel
            {
                Id = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ITStaff = category.ITStaff.UserName,
            };

            return View(model);
        }

        /// <summary>
        /// This action enables the deletion of a category.
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <returns> Category, Delete view</returns>
        // (POST: Category/Delete/5) 
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
