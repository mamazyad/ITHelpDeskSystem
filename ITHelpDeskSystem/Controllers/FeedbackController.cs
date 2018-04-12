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
            
                TicketViewModel model = new TicketViewModel
                {
                    Id = ticket.TicketId,
                };

            var possibleAnswer = new List<AnswerViewModel>
            {
                new AnswerViewModel {Id = 1, Text = "Very Unsatisfied"},
                new AnswerViewModel {Id = 2, Text = "Unsatisfied"},
                new AnswerViewModel {Id = 3, Text = "Neural"},
                new AnswerViewModel {Id = 4, Text = "Satisfied"},
                new AnswerViewModel {Id = 5, Text = "Very Satisfied"},
            };

            var criteria = new List<CriterionViewModel>
            {
                new CriterionViewModel  {Id = 5, Text = "Very Satisfied", PossibeAnswers = possibleAnswer},
            };

            var model1 = new FeedbackViewModel();

            foreach (var item in criteria)
            {
                model1.Criteria.Add(item);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    FeedbackId = model.Id,
                    FeedbackComment = model.FeedbackComment,
                    FeedbackDate = DateTime.Now,
                    StaffId = User.Identity.GetUserId<int>(),
                };

                foreach (var criterion in model.Criteria)
                {

                }

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index", "Ticket");
            }

            return View(model);
        }

    }
}
