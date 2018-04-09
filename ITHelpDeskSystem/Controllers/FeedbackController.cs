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
    public class FeedbackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Feedback(int? id)
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
                TicketViewModel model = new TicketViewModel
                {
                    Id = ticket.TicketId,
                    CategoryId = ticket.CategoryId,
                };
            }

            return View();
        }

        [HttpPost]
        public ActionResult Feedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    FeedbackId = model.Id,
                    FeedbackComment = model.FeedbackComment,
                    FeedbackDate = DateTime.Now,
                    GradeGiven = model.GradeGiven,
                    StaffId = User.Identity.GetUserId<int>(),
                };

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
