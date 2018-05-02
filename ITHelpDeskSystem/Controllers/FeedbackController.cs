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
        /// <returns>Feedback model on success</returns>
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

            if (ticket.FeedbackGiven == true || ticket.Status == TicketStatus.Resolved)
            {
                return RedirectToActionPermanent("FeedbackGiven", new { id = ticket.TicketId });
            }

            if (ticket.Status==TicketStatus.Open|| ticket.Status == TicketStatus.InProgress)
            {
                return RedirectToActionPermanent("FeedbackGiven", new { id = ticket.TicketId });
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
            decimal sum = 0;
            int count = 0;
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Find(Id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                foreach (var criterion in model.Criteria)
                {
                    if (criterion.SelectedAnswer.HasValue)
                    {
                        count++;
                        sum = sum + criterion.SelectedAnswer.Value;
                    }
                }
                var temp = User.Identity.GetUserId<int>();
                var feedback = new Feedback
                {
                    FeedbackId = model.Id,
                    FeedbackComment = model.FeedbackComment,
                    FeedbackDate = DateTime.Now,
                    FeedbackGiven =true,
                    StaffId = User.Identity.GetUserId<int>(),
                    TicketId = Id,
                    StaffName = db.Employees.Find(temp).FullName,
                    Grade = sum,
                };
                foreach (var criterion in model.Criteria)
                {
                    model.Id= criterion.Id;
                    model.SelectedAnswer = criterion.SelectedAnswer;
                    model.FeedbackComment = criterion.Text;
                }

                ticket.Status = TicketStatus.Resolved;
                ticket.FeedbackGiven = true;

                db.Feedbacks.Add(feedback);  db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Ticket");
            }
            return View(model);
        }

        /// <summary>
        /// This action informs  staff that ticket is open and feedback connot be given.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter</param>
        /// <returns></returns>
        public ActionResult Open(int? id)
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
            var model = new TicketViewModel
            {
                Id = ticket.TicketId,
                ResultionDate = ticket.ResultionDate,
                DueDate = ticket.DueDate,
            };
            return View(model);
        }


        /// <summary>
        /// This action informs staff that feedback has been given.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter</param>
        /// <returns></returns>
        public ActionResult FeedbackGiven(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            var model = new FeedbackViewModel
            {
                Id = feedback.FeedbackId,
                FeedbackDate = feedback.FeedbackDate,
            };
            return View(model);
        }

        /// <summary>
        /// This action provides IT manager with all the feedbacks given to am IT staff
        /// </summary>
        /// <param name="Id">IT staff ID</param>
        /// <returns>Feedback index on success </returns>
        [Authorize(Roles = "ITManager")]
        public ActionResult FeedbackIndex(int? Id)
        {
            var feedback = db.Feedbacks.Where(m => m.Ticket.Category.ITStaffId == Id).ToList();
            var model = new List<FeedbackViewModel>();
            foreach (var item in feedback)
            {
                model.Add(new FeedbackViewModel
                {
                    Id = item.FeedbackId,
                    FeedbackDate = item.FeedbackDate,
                    Grade = item.Grade,
                    FeedbackComment = item.FeedbackComment,
                    StaffId = item.StaffId,
                    StaffName = item.StaffName,
                });
            }
            return View(model);
        }
    }
}
