/*
* Description: This file contains the comment controller with the comment creation and edition methods (actions).
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
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comment/comment
        public ActionResult Comment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            TicketViewModel model = new TicketViewModel
            {
                Id = ticket.TicketId,
            };
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(int id, CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("ITStaff, Admin"))
                {
                    var comment = new Comment
                    {
                        CommentId = model.Id,
                        CommentText = model.CommentText,
                        ITStaffId = User.Identity.GetUserId<int>(),
                        Title = model.Title,
                        TicketId = id,
                    };
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Ticket");
                }
                if (User.IsInRole("Staff"))
                {
                    var comment = new Comment
                    {
                        CommentId = model.Id,
                        CommentDate = DateTime.Now,
                        CommentText = model.CommentText,
                        StaffId = User.Identity.GetUserId<int>(),
                        Title = model.Title,
                        TicketId = id,
                    };
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Ticket");
                }

            }
            return View(model);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            var model = new CommentViewModel
            {
                Id = comment.CommentId,
                CommentDate = comment.CommentDate,
                CommentText = comment.CommentText,
                UpdatedCommentText = comment.UpdatedCommentText,
                EditionDate = comment.EditionDate,
            };
            return View(model);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound();
                }
                comment.EditionDate = DateTime.Now;
                comment.UpdatedCommentText = model.UpdatedCommentText;

                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Ticket");
            }
            return View(model);
        }

        // GET: Comment/Delete/5. 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            CommentViewModel model = Mapper.Map<CommentViewModel>(comment);
            return View(model);
        }

        /// <summary>
        /// This action enables the deletion of a comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Comment, Delete view</returns>
        // (POST: Comment/Delete/5) 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", "Ticket");
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
