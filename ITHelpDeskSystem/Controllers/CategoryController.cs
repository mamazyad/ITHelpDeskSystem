/*
* Description: This file contains the category controller with the category creation, edition, deletion, listing and details methods (actions).
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
    /// Category controller manages the employees using Category and CategoryViewModel classes.
    /// </summary>
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all the categories created with the IT staff responsible for them. Category index view is based on it.
        /// </summary>
        /// <returns> Category, Index view</returns>
        // GET: Category 
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
                    ITStaff = item.ITStaff.FirstName + " " + item.ITStaff.LastName,
                });
            }
            return View(model);
        }
        /// <summary>
        ///  This action provides the details of a specific category. Category details view is based on it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category, Details view</returns>
        // GET: Category/Details/5
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
                ITStaff = category.ITStaff.UserName,
            };

            return View(model);
        }

        // GET: Category/Create. 
        public ActionResult Create()
        {
            var list = db.ITStaffs.Where(m => m.IsManager == false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");
            return View();
        }


        /// <summary>
        /// This action enables the creation of a category (with a unique name) and assigning it to a specific IT staff.
        /// </summary>
        /// <param name="model"></param>
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
            //HACK
            var list = db.ITStaffs.Where(m=>m.IsManager==false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");

            return View(model);
        }

        // GET: Category/Edit/5
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
            ViewBag.ITStaffId = new SelectList(db.ITStaffs, "Id", "UserName");
            return View(model);
        }
        /// <summary>
        /// This action enables the editing of a category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns> Category, Edit view</returns>
        // (POST: Category/Edit/5) 
        [HttpPost]
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
            ViewBag.ITStaffId = new SelectList(db.ITStaffs, "Id", "UserName");
            return View(model);
        }

        // GET: Category/Delete/5. 
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
        /// <param name="id"></param>
        /// <returns> Category, Delete view</returns>
        // (POST: Category/Delete/5) 
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
