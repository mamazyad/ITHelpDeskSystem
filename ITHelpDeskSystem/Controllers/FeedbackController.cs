/*
* Description: This file contains the Feedback controller, with the feedback method (action).
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
    public class FeedbackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action allows Staff to give feedback to service provided.
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <returns>Feedback model</returns>
        [Authorize(Roles = "Staff")]
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

            var possibleAnswers = new List<AnswerViewModel>
            {
                new AnswerViewModel {Id = 1, Text = "Very Unsatisfied"},
                new AnswerViewModel {Id = 2, Text = "Unsatisfied"},
                new AnswerViewModel {Id = 3, Text = "Neural"},
                new AnswerViewModel {Id = 4, Text = "Satisfied"},
                new AnswerViewModel {Id = 5, Text = "Very Satisfied"},
            };
            var criteria = db.Criteria.ToList();
            var modell = new List<CriterionViewModel>();
            foreach (var item in criteria)
            {
                modell.Add(new CriterionViewModel
                {
                    Id = item.CriterionId,
                    CriterionDescription = item.CriterionDescription,
                    PossibleAnswers = possibleAnswers,
                });
            }
            var modelx = new FeedbackViewModel();
            modelx.Criteria = modell;
            return View(modelx);
        }


        /// <summary>
        /// This action allows Staff to give feedback to service provided.
        /// </summary>
        /// <param name="model">Feedback View Model model</param>
        /// <returns>Ticket Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Feedback(int Id, FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Find(Id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                var feedback = new Feedback
                {
                    FeedbackId = model.Id,
                    FeedbackComment = model.FeedbackComment,
                    FeedbackDate = DateTime.Now,
                    StaffId = User.Identity.GetUserId<int>(),
                    TicketId = Id,
                };
                foreach (var criterion in model.Criteria)
                {
                    model.Id= criterion.Id;
                    model.SelectedAnswer = criterion.SelectedAnswer;
                }
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index", "Ticket");
            }
            return View(model);
        }
    }
}
