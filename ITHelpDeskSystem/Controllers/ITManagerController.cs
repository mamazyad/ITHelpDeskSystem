/*
* Description: This file contains the IT maanger index action, ticket details and IT staff details actions.
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
    public class ITManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all the IT staff to the IT manager with thier work load.
        /// </summary>
        /// <returns>IT staff view model on success </returns>
        // GET: ITManager
        [Authorize(Roles = "ITManager")]
        public ActionResult Index()
        {
            var ITstaffs = db.ITStaffs.Where(m => m.IsManager == false).ToList();
            var Admins = db.ITHelpDeskAdmins.ToList();
            var AllIT = Admins.Concat(ITstaffs);
            var model = new List<ITStaffViewModel>();
            foreach (var item in ITstaffs)
            {
                model.Add(new ITStaffViewModel
                {
                    Id = item.Id,
                    FullName2 = item.FullName,
                    CategoryLoad = db.Categories.Count(m => m.ITStaffId == item.Id),
                    TicketLoad = db.Tickets.Count(m => m.Category.ITStaffId == item.Id),
                });
            }
            return View(model);
        }


        /// <summary>
        /// This action displays the tickets an IT staff is responsible of.
        /// </summary>
        /// <param name="id">IT staff ID</param>
        /// <returns>Ticket view model on success </returns>
        // GET: ITManager
        [Authorize(Roles = "ITManager")]
        public ActionResult TechnicianTickets(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Tickets = db.Tickets.Where(m => m.Category.ITStaffId == id).ToList();
            var model = new List<TicketViewModel>();
            foreach (var item in Tickets)
            {
                model.Add(new TicketViewModel
                {
                    Id = item.TicketId,
                    Subject = item.Subject,
                    TicketOwnerName = item.StaffOwner.FullName,
                    IncidentDescription = item.IncidentDescription,
                    CreationDate = item.CreationDate,
                    Category = item.Category.CategoryName,
                    Status = item.Status,
                    DueDate = item.DueDate,
                    ResultionDate = item.ResultionDate,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action displays all the tickets created in the system.
        /// </summary>
        /// <returns>Ticket view model</returns>
        // GET: ITManager
        [Authorize(Roles = "ITManager")]
        public ActionResult AllTickets()
        {
            var Tickets = db.Tickets.ToList();
            var model = new List<TicketViewModel>();
            foreach (var item in Tickets)
            {
                model.Add(new TicketViewModel
                {
                    Id = item.TicketId,
                    Subject = item.Subject,
                    TicketOwnerName = item.StaffOwner.FullName,
                    IncidentDescription = item.IncidentDescription,
                    CreationDate = item.CreationDate,
                    Category = item.Category.CategoryName,
                    Status = item.Status,
                    DueDate = item.DueDate,
                    ResultionDate = item.ResultionDate,
                });
            }
            return View(model);
        }


        /// <summary>
        /// This action displays  the dtails of a ticket
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <returns>Ticket view model on success</returns>
        [Authorize(Roles = "ITManager")]
        public ActionResult TicketDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var model = new TicketViewModel
            {
                Id = ticket.TicketId,
                Subject = ticket.Subject,
                IncidentDescription = ticket.IncidentDescription,
                CreationDate = ticket.CreationDate,
                Category = ticket.Category.CategoryName,
                CreatedByName = ticket.Employee.FullName,
                Status = ticket.Status,
                TicketOwnerName = ticket.StaffOwner.FullName,
                Priority = ticket.Priority,
                DueDate = ticket.DueDate,
                IncidentSolution = ticket.IncidentSolution,
                AttachmentFilePath = ticket.AttachmentFilePath,
                ResultionDate = ticket.ResultionDate,
                AccelerationDate = ticket.AccelerationDate,
                AccelerationComment = ticket.AccelerationComment,
                ITStaffResponsibleName = ticket.Category.ITStaff.FullName,
            };
            return View(model);
        }

        /// <summary>
        /// This action will displpay the ticket information.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter.</param>
        /// <returns>Ticket info partial view</returns>
        public PartialViewResult AssignPartial(int? id)
        {
            var assignment = db.Assignments.Where(m => m.AssignmentId == id).ToList();
            var model = new List<AssignmentViewModel>();
            foreach (var item in assignment)
            {
                model.Add(new AssignmentViewModel
                {
                    Id = item.AssignmentId,
                    AssignedByName = item.AssignedByName,
                    AssignmentDate = item.AssignmentDate,
                    AssignmentComment = item.AssignmentComment,
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    AssignedTo = item.AssignedTo,
                });
            }
            return PartialView(model);
        }


    }
}
