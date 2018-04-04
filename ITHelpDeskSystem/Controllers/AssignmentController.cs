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

        // GET: Assignment/Create
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
           AssignmentViewModel model = new AssignmentViewModel
           {
                Id = ticket.TicketId,
            };

            //var list = db.ITStaffs.Where(m => m.IsManager == false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });
            //ViewBag.ITStaffId = new SelectList(list, "Id", "FullName");

            var list1 = db.Categories.Select(p => new { p.CategoryId, p.CategoryName, FullName = p.ITStaff.FirstName + " " + p.ITStaff.LastName });

            ViewBag.CategoryId = new SelectList(list1, "CategoryName", "Id", "FullName");
            return View(model);
        }

        // POST: Assignment/Create
        [HttpPost]
        public ActionResult Assign(AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var assignment = new Assignment
                {
                    AssignmentId = model.Id,
                    AssignmentComment =model.AssignmentComment,
                    AssignmentDate = DateTime.Now,
                    AssignedBy = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    CategoryId = model.CategoryId,
                    TicketId = model.TicketId,
                    ITStaffId = model.ITStaffId,
                };

                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var list = db.ITStaffs.Where(m => m.IsManager == false).Select(p => new { p.Id, FullName = p.FirstName + " " + p.LastName });

            var list1 = db.Categories.Select(p => new { p.CategoryId, p.CategoryName, FullName = p.ITStaff.FirstName + " " + p.ITStaff.LastName });

            ViewBag.CategoryId = new SelectList(list1, "CategoryName", "Id", "FullName");

            return View(model);
        }
    }
}
