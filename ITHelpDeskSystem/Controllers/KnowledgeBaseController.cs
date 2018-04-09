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

        // GET: KnowledgeBase
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
            var model = new KnowledgeBaseViewModel
            {
                Id = knowledgeBase.KnowledgeBaseId,
                CreatedByName = knowledgeBase.ITStaff.FullName,
                EditedByName = db.ITStaffs.Find(knowledgeBase.EditedBy).FullName,
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

        // GET: KnowledgeBase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KnowledgeBase/Create
        [HttpPost]
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
                // check if the uplaoded file is empty (do not upload empty files)
                if (model.KBAttachment != null && model.KBAttachment.ContentLength > 0)
                {
                    // Allowed extensions to be uploaded
                    var extensions = new[] { "pdf", "docx", "doc", "jpeg", "png", "jpg" };

                    // using System.IO for Path class
                    // Get the file name without the path
                    string filename = Path.GetFileName(model.KBAttachment.FileName);

                    // Get the extension of the file
                    string ext = Path.GetExtension(filename).Substring(1);

                    // Check if the extension of the file is in the list of allowed extensions
                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpeg, jpg and png documents");
                        return View();
                    }

                    // Set the application folder where to save the uploaded file
                    string appFolder = "~/Content/Uploads/";

                    // Generate a random string to add to the file name
                    // This is to avoid the files with the same names
                    var rand = Guid.NewGuid().ToString();

                    // Combine the application folder location with the file name
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

                    // Save the file in ~/Content/Uploads/filename.xyz
                    model.KBAttachment.SaveAs(path);

                    // Add the path to the course object
                    knowledgeBase.KBAttachmentFilePath = appFolder + rand + "-" + filename;

                }
                db.KnowledgeBases.Add(knowledgeBase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: KnowledgeBase/Edit/5
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
                //KBAttachmentFilePath = knowledgeBase.KBAttachmentFilePath,
            };
            return View(model);
        }

        // POST: KnowledgeBase/Edit/5
        [HttpPost]
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

                db.Entry(knowledgeBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: KnowledgeBase/Delete/5
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
            KnowledgeBaseViewModel model = new KnowledgeBaseViewModel
            {
                Id = knowledgeBase.KnowledgeBaseId,
                CreatedByName = knowledgeBase.ITStaff.FullName,
                EditedByName = knowledgeBase.ITStaff.FullName,
                Topic = knowledgeBase.Topic,
                IncidentTitle = knowledgeBase.IncidentTitle,
                IncidentDescription = knowledgeBase.IncidentDescription,
                SolutionDescription = knowledgeBase.SolutionDescription,
                CreationDate = knowledgeBase.CreationDate,
                EditionDate = knowledgeBase.EditionDate,
                //KBAttachmentFilePath = knowledgeBase.KBAttachmentFilePath,
            };
            return View(model);
        }

        // POST: KnowledgeBase/Delete/5
        [HttpPost, ActionName("Delete")]
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
    }
}
