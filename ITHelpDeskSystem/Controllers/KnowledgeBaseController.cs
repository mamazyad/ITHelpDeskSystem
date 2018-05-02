/*
* Description: This file contains the knowledge base controller with the solution creation, edition, deletion, listing and details methods (actions).
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
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ITHelpDeskSystem.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action list all the solutions in the Knowldge Base.
        /// </summary>
        /// <returns>Knowledge Base Index.</returns>
        // GET: Knowledge Base
        public ActionResult Index()
        {
            var knowledgeBase = db.KnowledgeBases.ToList();
            var model = new List<KnowledgeBaseViewModel>();
            foreach (var item in knowledgeBase)
            {
                model.Add(new KnowledgeBaseViewModel
                {
                    Id = item.KnowledgeBaseId,
                    CreationDate = item.CreationDate,
                    EditionDate = item.EditionDate,
                    Topic = item.Topic,
                    IncidentTitle = item.IncidentTitle,
                    IncidentDescription = item.IncidentDescription,
                    SolutionDescription = item.SolutionDescription,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action lists the details of a specific Knowldge Base.
        /// </summary>
        /// <param name="id">Knowledge Base ID</param>
        /// <returns>Knowldge Base Details</returns>
        // GET: KnowledgeBase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgeBase knowledgeBase = db.KnowledgeBases.Find(id);

            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }

            string temp = null;

            if (!(knowledgeBase.EditedBy == null))
            {
                temp = db.ITStaffs.Find(knowledgeBase.EditedBy).FullName;
            }

            if (knowledgeBase.EditedBy == null)
            {
                temp = "";
            }

            var model = new KnowledgeBaseViewModel
            {
                Id = knowledgeBase.KnowledgeBaseId,
                CreatedByName = knowledgeBase.ITStaff.FullName,
                EditedBy = User.Identity.GetUserId<int>(),
                EditedByName = temp,
                Topic = knowledgeBase.Topic,
                IncidentTitle = knowledgeBase.IncidentTitle,
                IncidentDescription = knowledgeBase.IncidentDescription,
                SolutionDescription = knowledgeBase.SolutionDescription,
                CreationDate = knowledgeBase.CreationDate,
                EditionDate = knowledgeBase.EditionDate,
                KBAttachmentFilePath = knowledgeBase.KBAttachmentFilePath,
            };
            return View(model);
        }

        /// <summary>
        /// This action allows the creation of a knoldge base solution.
        /// </summary>
        /// <returns>Knowldge base create</returns>
        // GET: KnowledgeBase/Create
        [Authorize(Roles = "ITStaff, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action allows the creation of a knoldge base solution.
        /// </summary>
        /// <param name="model">KnowledgeBaseViewModel model</param>
        /// <returns>Knowledge Base Index.</returns>
        // POST: KnowledgeBase/Create
        [HttpPost]
        [Authorize(Roles = "ITStaff, Admin")]
        [ValidateInput(false)]
        public ActionResult Create(KnowledgeBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var knowledgeBase = new KnowledgeBase
                {
                    KnowledgeBaseId = model.Id,
                    Topic = model.Topic,
                    IncidentTitle = model.IncidentTitle,
                    IncidentDescription = model.IncidentDescription,
                    SolutionDescription = model.SolutionDescription,
                    CreationDate = DateTime.Now,
                    CreatedBy = User.Identity.GetUserId<int>(),
                    ITStaffId = User.Identity.GetUserId<int>(),
                };

                if (model.KBAttachment != null && model.KBAttachment.ContentLength > 0)
                {
                    var extensions = new[] { "pdf", "docx", "doc", "jpeg", "png", "jpg" };

                    string filename = Path.GetFileName(model.KBAttachment.FileName);

                    string ext = Path.GetExtension(filename).Substring(1);

                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpeg, jpg and png documents");
                        return View();
                    }

                    string appFolder = "~/Content/Uploads/";

                    var rand = Guid.NewGuid().ToString();

                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

                    model.KBAttachment.SaveAs(path);

                    knowledgeBase.KBAttachmentFilePath = appFolder + rand + "-" + filename;

                }
                db.KnowledgeBases.Add(knowledgeBase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// This action allows for knowldge base edition.
        /// </summary>
        /// <param name="id">Knowldge Base ID</param>
        /// <returns>Knowldge Base edit view</returns>
        // GET: KnowledgeBase/Edit/5
        [Authorize(Roles = "ITStaff, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KnowledgeBase knowledgeBase = db.KnowledgeBases.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            KnowledgeBaseViewModel model = new KnowledgeBaseViewModel
            {
                Id = knowledgeBase.KnowledgeBaseId,
                Topic = knowledgeBase.Topic,
                IncidentTitle = knowledgeBase.IncidentTitle,
                IncidentDescription = knowledgeBase.IncidentDescription,
                SolutionDescription = knowledgeBase.SolutionDescription,
                EditedBy = knowledgeBase.EditedBy,
                EditionDate = knowledgeBase.EditionDate,
                KBAttachmentFilePath = knowledgeBase.KBAttachmentFilePath,
            };
            return View(model);
        }

        /// <summary>
        /// This action allows for knowldge base edition.
        /// </summary>
        /// <param name="id">Knowldge Base ID</param>
        /// <param name="model">KnowledgeBaseViewModel model</param>
        /// <returns>Knowldge Base Index</returns>
        // POST: KnowledgeBase/Edit/5
        [HttpPost]
        [Authorize(Roles = "ITStaff, Admin")]
        [ValidateInput(false)]
        public ActionResult Edit(int id, KnowledgeBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                KnowledgeBase knowledgeBase = db.KnowledgeBases.Find(id);
                if (knowledgeBase == null)
                {
                    return HttpNotFound();
                }
                knowledgeBase.Topic = model.Topic;
                knowledgeBase.IncidentTitle = model.IncidentTitle;
                knowledgeBase.IncidentDescription = model.IncidentDescription;
                knowledgeBase.SolutionDescription = model.SolutionDescription;
                knowledgeBase.EditionDate = DateTime.Now;
                knowledgeBase.EditedBy = User.Identity.GetUserId<int>();

                if (model.KBAttachment != null && model.KBAttachment.ContentLength > 0)
                {
                    var extensions = new[] { "pdf", "docx", "doc", "jpeg", "png", "jpg" };

                    string filename = Path.GetFileName(model.KBAttachment.FileName);

                    string ext = Path.GetExtension(filename).Substring(1);

                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpeg, jpg and png documents");
                        return View();
                    }

                    string appFolder = "~/Content/Uploads/";

                    var rand = Guid.NewGuid().ToString();

                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

                    model.KBAttachment.SaveAs(path);

                    knowledgeBase.KBAttachmentFilePath = appFolder + rand + "-" + filename;
                }

                db.Entry(knowledgeBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// This action allows for Knowldge Base deletion.
        /// </summary>
        /// <param name="id">Knowldge Base ID</param>
        /// <returns>Knowldge Base delete</returns>
        // GET: KnowledgeBase/Delete/5
        [Authorize(Roles = "ITStaff, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            KnowledgeBase knowledgeBase = db.KnowledgeBases.Find(id);
            if (knowledgeBase == null)
            {
                return HttpNotFound();
            }
            string temp = null;

            if (!(knowledgeBase.EditedBy == null))
            {
                temp = db.ITStaffs.Find(knowledgeBase.EditedBy).FullName;
            }

            if (knowledgeBase.EditedBy == null)
            {
                temp = "";
            }
            KnowledgeBaseViewModel model = new KnowledgeBaseViewModel
            {
                Id = knowledgeBase.KnowledgeBaseId,
                CreatedByName = knowledgeBase.ITStaff.FullName,
                EditedBy = User.Identity.GetUserId<int>(),
                EditedByName = temp,
                Topic = knowledgeBase.Topic,
                IncidentTitle = knowledgeBase.IncidentTitle,
                IncidentDescription = knowledgeBase.IncidentDescription,
                SolutionDescription = knowledgeBase.SolutionDescription,
                CreationDate = knowledgeBase.CreationDate,
                EditionDate = knowledgeBase.EditionDate,
                KBAttachmentFilePath = knowledgeBase.KBAttachmentFilePath,
            };
            return View(model);
        }

        /// <summary>
        /// This action allows for Knowldge Base deletion.
        /// </summary>
        /// <param name="id">Knowldge Base ID</param>
        /// <returns>Knowldge Base Index</returns>
        // POST: KnowledgeBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "ITStaff, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KnowledgeBase knowledgeBase = db.KnowledgeBases.Find(id);
            db.KnowledgeBases.Remove(knowledgeBase);
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

        public ActionResult Search(string searchString)
        {
            var solutions = db.KnowledgeBases.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                var result = solutions.Where(s => s.IncidentDescription.Contains(searchString));
            }

            return View("Index");
        }
    }
}
