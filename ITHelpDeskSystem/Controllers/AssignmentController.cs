﻿/*
* Description: This file contains the assignment controller with the assignment method (action).
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
    public class AssignmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action allows for ticket reassignment by changing the ticket category.
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Ticket Index on success</returns>
        // GET: Assignment/Create
        [Authorize(Roles = "ITStaff, Admin")]
        public ActionResult Assign(int? id)
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
                return RedirectToActionPermanent("Closed", "Ticket", new { id = ticket.TicketId });
            }
            TicketViewModel model = new TicketViewModel
           {
                Id = ticket.TicketId,
                CategoryId = ticket.CategoryId,
            };

            var list = db.Categories.Where(m => m.CategoryId != ticket.CategoryId);
            ViewBag.CategoryId = new SelectList(list, "CategoryId", "CategoryName");
            return View();
        }

        /// <summary>
        /// This action allows for ticket reassignment by changing the ticket category.
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Ticket Index on success</returns>
        // POST: Assignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ITStaff")]
        public ActionResult Assign(int id, AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                ticket.CategoryId = model.CategoryId;
                var temp = User.Identity.GetUserId<int>();
                var assignment = new Assignment
                {
                    AssignmentId = model.Id,
                    AssignmentDate = DateTime.Now,
                    AssignedBy = User.Identity.GetUserId<int>(),
                    AssignedByName = db.Employees.Find(temp).FullName,
                    AssignmentComment = model.AssignmentComment,
                    CategoryId = model.CategoryId,
                    TicketId = ticket.TicketId,
                };

                var list = db.Categories.Where(m => m.CategoryId != ticket.CategoryId);
                ViewBag.CategoryId = new SelectList(list, "CategoryId", "CategoryName");

                db.Assignments.Add(assignment);
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Ticket");
            }
            return View(model);
        }
    }
}
