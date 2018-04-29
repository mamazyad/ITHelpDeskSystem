/*
* Description: 
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
                        FirstName = item.FullName,
                        CategoryLoad = db.Categories.Count(m => m.ITStaffId == item.Id),
                        TicketLoad = db.Tickets.Count(m => m.Category.ITStaffId == item.Id),
                    });
            }
            return View(model);
        }


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


        [Authorize(Roles = "ITManager")]
        public ActionResult TicketDetails(int? id)
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
                Email = ticket.StaffOwner.Email,
            };

            return View(model);
        }


        //public static TimeSpan? Difference(DateTime resulotion, DateTime DueDate)
        //{
        //    TimeSpan? Difference = resulotion - DueDate;
        //    return Difference;
        //}

        //public static TimeSpan Average(this IEnumerable<TimeSpan> timeSpans)
        //{
        //    IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
        //    double averageTicks = ticksPerTimeSpan.Average();
        //    long averageTicksLong = Convert.ToInt64(averageTicks);

        //    TimeSpan averageTimeSpan = TimeSpan.FromTicks(averageTicksLong);

        //    return averageTimeSpan;
        //}


    }
}
