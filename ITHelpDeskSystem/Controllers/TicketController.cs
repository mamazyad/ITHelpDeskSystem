/*
* Description: This file is the ticket controller, enabling the ticket creation (for staff and for IT help desk admin on behalf of staff), edition, listing and details methods (actions).
* Author: mamazyad
*/

using AutoMapper;
using ITHelpDeskSystem.Models;
using ITHelpDeskSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITHelpDeskSystem.Controllers
{
    public class TicketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ticket
        public ActionResult Index()
        {
            var tickets = db.Tickets.ToList();
            var model = new List<TicketViewModel>();

            foreach (var item in tickets)
            {
                model.Add(new TicketViewModel
                {
                    Id = item.TicketId,
                    Subject = item.Subject,
                    IncidentDescription = item.IncidentDescription,
                    //HACK Display the creation date
                    CreationDate = item.CreationDate,
                    Category = item.Category.CategoryName,
                    //CreatedBy = item.CreatedBy, //HACK May be get the name of the creator instead of her Id
                    CreatedByName = item.Employee.FullName,
                    //TicketOwnerName = item.Staff.FullName,

                });
            }
            return View(model);
        }



        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Ticket/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        public ActionResult Create(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    TicketId = model.Id,
                    Subject = model.Subject,
                    IncidentDescription = model.IncidentDescription,
                    CategoryId = model.CategoryId,
                    //HACK Add the current date
                    CreationDate = DateTime.Now,
                    //HACK You need to be logged in as employee/staff to assign the creator id automatically
                    //CreatedBy = User.Identity.GetUserId<int>(),
                    // For now you can use the below
                    CreatedBy = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                };

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(model);
        }

        // GET: Ticket/CreateONBehalf
        public ActionResult CreateONBehalf()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "FullName");
            return View();
        }

        // POST: Ticket/CreateONBehalf
        [HttpPost]
        public ActionResult CreateONBehalf(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    TicketId = model.Id,
                    Subject = model.Subject,
                    IncidentDescription = model.IncidentDescription,
                    CategoryId = model.CategoryId,
                    //HACK Add the current date
                    CreationDate = DateTime.Now,
                    //StaffId = model.StaffId,
                    //HACK You need to be logged in as employee/staff to assign the creator id automatically
                    //CreatedBy = User.Identity.GetUserId<int>(),
                    // For now you can use the below
                    CreatedBy = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                };

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "FullName");

            return View(model);
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
