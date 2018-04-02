/*
* Description: This file contains the ticket controller, enabling the ticket creation (for staff and for IT help desk admin on behalf of staff), edition, listing and details methods (actions).
* Author: mamazyad
* Date: 20/03/2018
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
    /// <summary>
    /// Ticket controller manages the Tickets using Ticket and TicketViewModel classes.
    /// </summary>

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
                    CreationDate = item.CreationDate,
                    Category = item.Category.CategoryName,
                    CreatedByName = item.Employee.FullName,
                    Status = item.Status,
                    TicketOwnerName = item.StaffOwner.FullName,
                    Priority = item.Priority,
                    DueDate = item.DueDate,
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
        /// This action enables Staff user to creat of a ticket.
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
                    CreationDate = DateTime.Now,
                    Status = TicketStatus.Open,
                    //CreatedBy = User.Identity.GetUserId<int>(),
                    CreatedBy = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    TicketOwner = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                };

                db.Tickets.Add(ticket);
                db.SaveChanges();
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

                // check if the uplaoded file is empty (do not upload empty files)
                if (model.Attachment != null && model.Attachment.ContentLength > 0)
                {
                    // Allowed extensions to be uploaded
                    var extensions = new[] { "pdf", "docx", "doc", "jpeg", "png", "jpg" };

                    // using System.IO for Path class
                    // Get the file name without the path
                    string filename = Path.GetFileName(model.Attachment.FileName);

                    // Get the extension of the file
                    string ext = Path.GetExtension(filename).Substring(1);

                    // Check if the extension of the file is in the list of allowed extensions
                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpeg, jpg and png documents");
                        return View();
                    }

                    // Set the application folder where to save the uploaded file
                    string appFolder = "~/Content/Uploads/";

                    // Generate a random string to add to the file name
                    // This is to avoid the files with the same names
                    var rand = Guid.NewGuid().ToString();

                    // Combine the application folder location with the file name
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

                    // Save the file in ~/Content/Uploads/filename.xyz
                    model.Attachment.SaveAs(path);

                    // Add the path to the course object
                    ticket.AttachmentFilePath = appFolder + rand + "-" + filename;

                }
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        // (GET: Ticket/CreateOnBehalf) 
        public ActionResult CreateOnBehalf()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.TicketOwner = new SelectList(db.Staffs, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action allows the Admin to create a ticket on behalf of Staff.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ticket, CreateOnBehalf view</returns>
        // (POST: Ticket/CreateOnBehalf) This action enables ITHelpDeskAdmin to creat of a ticket on behalf of a staff.
        [HttpPost]
        public ActionResult CreateOnBehalf(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    TicketId = model.Id,
                    Subject = model.Subject,
                    IncidentDescription = model.IncidentDescription,
                    CategoryId = model.CategoryId,
                    CreationDate = DateTime.Now,
                    Status = TicketStatus.Open,
                    //CreatedBy = User.Identity.GetUserId<int>(),
                    CreatedBy = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id,
                    TicketOwner = model.TicketOwner,
                };

                db.Tickets.Add(ticket);
                db.SaveChanges();
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.TicketOwner = new SelectList(db.Staffs, "Id", "FullName");

                // check if the uplaoded file is empty (do not upload empty files)
                if (model.Attachment != null && model.Attachment.ContentLength > 0)
                {
                    // Allowed extensions to be uploaded
                    var extensions = new[] { "pdf", "docx", "doc", "jpeg", "png", "jpg" };

                    // using System.IO for Path class
                    // Get the file name without the path
                    string filename = Path.GetFileName(model.Attachment.FileName);

                    // Get the extension of the file
                    string ext = Path.GetExtension(filename).Substring(1);

                    // Check if the extension of the file is in the list of allowed extensions
                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpeg, jpg and png documents");
                        return View();
                    }

                    // Set the application folder where to save the uploaded file
                    string appFolder = "~/Content/Uploads/";

                    // Generate a random string to add to the file name
                    // This is to avoid the files with the same names
                    var rand = Guid.NewGuid().ToString();

                    // Combine the application folder location with the file name
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

                    // Save the file in ~/Content/Uploads/filename.xyz
                    model.Attachment.SaveAs(path);

                    // Add the path to the course object
                    ticket.AttachmentFilePath = appFolder + rand + "-" + filename;

                }
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.TicketOwner = new SelectList(db.Staffs, "Id", "FullName");
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// This action allows the Admin and IT staff to edit a ticket's details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Ticket/Edit/5
        public ActionResult Edit(int? id)
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
                return RedirectToActionPermanent("NotFound", new { id = ticket.TicketId });
            }

            EditTicketViewModel model = new
                EditTicketViewModel
            {
                Id = ticket.TicketId,
                Priority = ticket.Priority,
                Status = ticket.Status,
                IncidentSolution = ticket.IncidentSolution,
                DueDate = ticket.DueDate,
                ResultionDate = ticket.ResultionDate,
                //Subject = ticket.Subject,
                //IncidentDescription = ticket.IncidentDescription,
                //CreationDate = ticket.CreationDate,
                //TicketOwner = ticket.TicketOwner,
                //CategoryId = ticket.CategoryId,
                //Category = ticket.Category.CategoryName,
                //TicketOwnerName = ticket.StaffOwner.FullName,
                //CreatedByName = ticket.Employee.FullName,
            };
            return View(model);
        }

        /// <summary>
        /// This action allows IT staff (including Admin) to edit/update ticket.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns>Ticket, Edit view</returns>
        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TicketViewModel model1, EditTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }

                ticket.Priority = model.Priority;
                ticket.IncidentSolution = model.IncidentSolution;


                if (!(model.Priority == null))
                {
                    ticket.Status = TicketStatus.InProgress;
                }
                if (model.Priority == TicketPriority.Critical)
                {
                    ticket.DueDate = model1.CreationDate.AddDays(1);
                }
                else if (model.Priority == TicketPriority.High)
                {
                    ticket.DueDate = model1.CreationDate.AddDays(3);
                }
                else if (model.Priority == TicketPriority.Medium)
                {
                    ticket.DueDate = model1.CreationDate.AddDays(5);
                }
                else if (model.Priority == TicketPriority.Low)
                {
                    ticket.DueDate = model1.CreationDate.AddDays(7);
                }


                if (!(model.IncidentSolution == null))
                {
                    ticket.Status = TicketStatus.Closed;
                    ticket.ResultionDate = DateTime.Now;
                }

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        /// <summary>
        /// This action informs IT staff that ticket in closed and cannot be edited.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult NotFound(int? id)
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
            };

            return View(model);
        }

        /// <summary>
        /// This action will displpay the ticket information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult TicketInfoPartial(int id)
        {
            var items = db.Tickets.Where(d => d.TicketId == id).ToList();
            var model = new List<TicketViewModel>();
            foreach (var item in items)
            {
                model.Add(new TicketViewModel
                {
                    Id = item.TicketId,
                    Subject = item.Subject,
                    IncidentDescription = item.IncidentDescription,
                    CreationDate = item.CreationDate,
                    Category = item.Category.CategoryName,
                    CreatedByName = item.Employee.FullName,
                    Status = item.Status,
                    TicketOwnerName = item.StaffOwner.FullName,
                    DueDate = item.DueDate,
                    //DueDate = item.DueDate,

                });
            }

            return PartialView(model);
        }
    }
}
