/*
* Description: This file contains the ticket controller, enabling the ticket creation (for staff and for IT help desk admin on behalf of staff), edition, listing and details methods (actions).
* Author: mamazyad
*  Due date: 20/03/2018
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


        /// <summary>
        /// This action lists all the tickets assigned to an IT staff, based on the category the IT staff is responsible for.
        /// </summary>
        /// <returns>Ticket, Index view</returns>
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
                    //TicketOwnerName = item.StaffOwner.FullName,

                });
            }
            return View(model);
        }


        /// <summary>
        /// This action displays the details of a spicific ticket.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ticket, Details view</returns>
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
        
        /// <summary>
        /// This action enables Staff member to creat of a ticket.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ticket, Create view</returns>
        // (POST: Ticket/Create)
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

        // (GET: Ticket/CreateONBehalf) 
        public ActionResult CreateONBehalf()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action allows the Admin to create a ticket on behalf of Staff.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ticket, CreateOnBehalf view</returns>
        // (POST: Ticket/CreateONBehalf) This action enables ITHelpDeskAdmin to creat of a ticket on behalf of a staff.
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

        /// <summary>
        /// This action allows IT staff 9including Admin) to edit/update ticket.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns>Ticket, Edit view</returns>
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
