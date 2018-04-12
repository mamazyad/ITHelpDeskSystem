/*
* Description: This file contains the criterion controller with the criterion creation, edition, deletion, listing and details methods (actions).
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
    public class CriterionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Criterion
        public ActionResult Index()
        {
            var criteria = db.Criteria.ToList();
            var model = new List<CriterionViewModel>();
            foreach (var item in criteria)
            {
                model.Add(new CriterionViewModel
                {
                    Id = item.CriterionId,
                    CreationDate = item.CreationDate,
                    ActiveCriterion = item.ActiveCriterion,
                    CriterionDescription = item.CriterionDescription,
                    CriterionName = item.CriterionName,
                    //TargetGrade = item.TargetGrade,
                    EditionDate = item.EditionDate,
                });
            }
            return View(model);
        }

        // GET: Criterion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterion criterion = db.Criteria.Find(id);
            if (criterion == null)
            {
                return HttpNotFound();
            }
            var model = new CriterionViewModel
            {
                Id = criterion.CriterionId,
                //TargetGrade = criterion.TargetGrade,
                CreationDate = criterion.CreationDate,
                CriterionDescription = criterion.CriterionDescription,
                CriterionName = criterion.CriterionName,
                EditionDate = criterion.EditionDate,
                ActiveCriterion = criterion.ActiveCriterion,
            };

            return View(model);
        }

        // GET: Criterion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Criterion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriterionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var criterion = new Criterion
                {
                    CriterionId = model.Id,
                    CriterionName = model.CriterionName,
                    CriterionDescription = model.CriterionDescription,
                    CreationDate = DateTime.Now,
                   // TargetGrade = model.TargetGrade,
                    ActiveCriterion = true,
                };

                db.Criteria.Add(criterion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Criterion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Criterion criterion = db.Criteria.Find(id);
            if (criterion == null)
            {
                return HttpNotFound();
            }
            var model = new CriterionViewModel
            {
                Id = criterion.CriterionId,
                //TargetGrade = criterion.TargetGrade,
                CreationDate = criterion.CreationDate,
                CriterionDescription = criterion.CriterionDescription,
                CriterionName = criterion.CriterionName,
                EditionDate = criterion.EditionDate,
                ActiveCriterion = criterion.ActiveCriterion,
            };
            return View(model);
    }

        // POST: Criterion/Edit/5
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CriterionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Criterion criterion = db.Criteria.Find(id);
            if (criterion == null)
            {
                return HttpNotFound();
            }
                criterion.CriterionName = model.CriterionName;
                criterion.CriterionDescription = model.CriterionDescription;
                criterion.CreationDate = model.CreationDate;
                //criterion.TargetGrade = model.TargetGrade;
                criterion.ActiveCriterion = model.ActiveCriterion;
                criterion.EditionDate = DateTime.Now;
            db.Entry(criterion).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
            return View(model);
    }
    }
}
