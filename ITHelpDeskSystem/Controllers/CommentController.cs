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

        /// <summary>
        /// This action allows for commenting on a ticket.
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns> Comment view model on sucess</returns>
        // GET: Comment/comment
        [Authorize(Roles = "ITStaff, Admin, Staff")]
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
            if (ticket.Status == TicketStatus.Closed)
            {
                return RedirectToActionPermanent("Closed","Ticket", new { id = ticket.TicketId });
            }
            CommentViewModel model = new CommentViewModel
            {
                Id = ticket.TicketId,
            };

            var comments = db.Comments.Where(p => p.TicketId == model.Id).ToList();
            var model1 = new List<CommentViewModel>();
            foreach (var item in comments)
            {
                model1.Add(new CommentViewModel
                {
                    Id = item.CommentId,
                    CommentDate = item.CommentDate,
                    CommentText = item.CommentText,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action allows for commenting on a ticket.
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns> Comment view model on sucess</returns>
        // POST: Comment/Create
        [HttpPost]
        [Authorize(Roles = "ITStaff, Admin, Staff")]
        public ActionResult Comment(int id, CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("ITStaff"))
                {
                    var temp = User.Identity.GetUserId<int>();
                    var comment = new Comment
                    {
                        CommentId = model.Id,
                        CommentText = model.CommentText,
                        CommenterId = User.Identity.GetUserId<int>(),
                        Commenter = db.Employees.Find(temp).FullName,
                        Title = model.Title,
                        TicketId = id,
                        CommentDate =DateTime.Now,
                    };
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Ticket");
                }
                if (User.IsInRole("Admin"))
                {
                    var temp = User.Identity.GetUserId<int>();
                    var comment = new Comment
                    {
                        CommentId = model.Id,
                        CommentText = model.CommentText,
                        CommenterId = User.Identity.GetUserId<int>(),
                        Commenter = db.Employees.Find(temp).FullName,
                        Title = model.Title,
                        TicketId = id,
                        CommentDate = DateTime.Now,
                    };
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Ticket");
                }
                if (User.IsInRole("Staff"))
                {
                    var temp = User.Identity.GetUserId<int>();
                    var comment = new Comment
                    {
                        CommentId = model.Id,
                        CommentDate = DateTime.Now,
                        CommentText = model.CommentText,
                        CommenterId = User.Identity.GetUserId<int>(),
                        Commenter = db.Employees.Find(temp).FullName,
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

        /// <summary>
        /// This action allows for the edition of a comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <returns>Comment view model on sucess</returns>
        // GET: Comment/Edit/5
        [Authorize(Roles = "ITStaff, Admin, Staff")]
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

        /// <summary>
        /// This action allows for the edition of a comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <returns>Comment view model on sucess</returns>
        // POST: Comment/Edit/5
        [HttpPost]
        [Authorize(Roles = "ITStaff, Admin, Staff")]
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

        /// <summary>
        /// This action enables the deletion of a comment.
        /// </summary>
        /// <param name="id">Comment ID</param>
        /// <returns> Comment, Delete view</returns>
        // GET: Comment/Delete/5. 
        [Authorize(Roles = "ITStaff, Admin, Staff")]
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
        /// <param name="id">Comment ID</param>
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
