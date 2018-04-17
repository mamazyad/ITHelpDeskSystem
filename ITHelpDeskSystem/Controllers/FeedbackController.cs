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

            ////HACK Text is the question/Criterion name
            //var criteria = new List<CriterionViewModel>
            //{
            //    new CriterionViewModel  {Id = 1, Text = "Criaterion Name 1", PossibleAnswers = possibleAnswers},
            //    new CriterionViewModel  {Id = 2, Text = "Criaterion Name 2", PossibleAnswers = possibleAnswers},
            //};

            //var model1 = new FeedbackViewModel();

            //foreach (var item in criteria)
            //{
            //    model1.Criteria.Add(item);
            //}

            var criterion = db.Criteria.ToList();
            var modell = new FeedbackViewModel();
            var modelX = new List<CriterionViewModel>();
            foreach (var item in criterion)
            {
              //  modell.Criteria.Add(item);
                // modelX.Add(new FeedbackViewModel
               // {
                  //  Id = item.CriterionId,
                   
                    //CriterionDescription=item.CriterionDescription,
                    //PossibleAnswers = possibleAnswers,
               // });
            }


            return View(modelX);
        }

        /// <summary>
        /// This action allows Staff to give feedback to service provided.
        /// </summary>
        /// <param name="model">FeedbackViewModel model</param>
        /// <returns>Ticket Index</returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Staff")]
        //public ActionResult Feedback(FeedbackViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var feedback = new Feedback
        //        {
        //            FeedbackId = model.Id,
        //            FeedbackComment = model.FeedbackComment,
        //            FeedbackDate = DateTime.Now,
        //            StaffId = User.Identity.GetUserId<int>(),
        //        };

        //        foreach (var criterion in model.Criteria)
        //        {

        //        }

        //        db.Feedbacks.Add(feedback);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Ticket");
        //    }

        //    return View(model);
        //}

    }
}
