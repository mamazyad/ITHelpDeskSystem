/*
* Description: This file contains the ticket controller, enabling the ticket creation (for staff and for IT help desk admin on behalf of staff), edition, listing and details methods (actions).
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
using System.Net.Mail;
using System.Web.Mvc;
using System.Configuration;
using System.Threading.Tasks;


namespace ITHelpDeskSystem.Controllers
{
    /// <summary>
    /// Ticket controller manages the Tickets using Ticket and TicketViewModel classes.
    /// </summary>

    public class TicketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all the tickets pertaining to the user logged in. In case of IT staff, based on the category the IT staff is responsible for, Staff will see the tickets they have created while Admin can see all the tickets created.
        /// </summary>
        /// <returns>Ticket, Index view</returns>
        // GET: Ticket
        [Authorize(Roles = "Staff, ITStaff, Admin")]
        public ActionResult Index()
        {
            var isAdmin = User.IsInRole("Admin");
            var user = User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : db.Users.First().Id;
            var tickets = db.Tickets.Where(m => m.Category.ITStaffId == user || user == m.TicketOwner || isAdmin == true).ToList();
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
        /// This action displays the details of a spicific ticket as seen by the IT staff and the admin.
        /// </summary>
        /// <param name="id">Details takes the ticket ID as a parameter</param>
        /// <returns>Ticket, Details view</returns>
        // GET: Ticket/Details/5
        [Authorize(Roles = "ITStaff, Admin, ITManager")]
        public ActionResult ITDetails(int? id)
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
            };
            return View(model);
        }


        /// <summary>
        /// This action displays the details of a spicific ticket as seen by Staff.
        /// </summary>
        /// <param name="id">Details takes the ticket ID as a parameter</param>
        /// <returns>Ticket, Details view</returns>
        // GET: Ticket/Details/5
        [Authorize(Roles = "Staff")]
        public ActionResult StaffDetails(int? id)
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
                Subject = ticket.Subject,
                IncidentDescription = ticket.IncidentDescription,
                CreationDate = ticket.CreationDate,
                CreatedByName = ticket.Employee.FullName,
                Category = ticket.Category.CategoryName,
                Status = ticket.Status,
                Priority = ticket.Priority,
                DueDate = ticket.DueDate,
                IncidentSolution = ticket.IncidentSolution,
                AttachmentFilePath = ticket.AttachmentFilePath,
                ResultionDate = ticket.ResultionDate,
                ITStaffResponsibleName = ticket.Category.ITStaff.FullName,
            };
            return View(model);
        }

        /// <summary>
        /// This action allows ticket creation.
        /// </summary>
        /// <returns>Ticket Create</returns>
        // GET: Ticket/Create
        [Authorize(Roles = "Staff")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        /// <summary>
        /// This action enables Staff user to creat of a ticket.
        /// </summary>
        /// <param name="model">Create takes the ticket view model as a parameter.</param>
        /// <returns>Ticket, Create view</returns>
        // (POST: Ticket/Create)
        [HttpPost]
        [Authorize(Roles = "Staff")]
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
                    CreatedBy = User.Identity.GetUserId<int>(),
                    TicketOwner = User.Identity.GetUserId<int>(),
                };

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
                db.Tickets.Add(ticket);
                db.SaveChanges();
                var owner = db.Employees.Find(ticket.TicketOwner).Email;
                //await MessageServices.SendEmail(owner, "f", "d");
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        // (GET: Ticket/CreateOnBehalf) 
        [Authorize(Roles = "Admin")]
        public ActionResult CreateOnBehalf()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.TicketOwner = new SelectList(db.Staffs, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action allows the Admin to create a ticket on behalf of Staff.
        /// </summary>
        /// <param name="model">Ticket view model</param>
        /// <returns>Ticket, CreateOnBehalf view</returns>
        // (POST: Ticket/CreateOnBehalf) This action enables ITHelpDeskAdmin to creat of a ticket on behalf of a staff.
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                    CreatedBy = User.Identity.GetUserId<int>(),
                    TicketOwner = model.TicketOwner,
                };

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
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// This action allows the Admin and IT staff to edit a ticket's details.
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <returns>Ticket edit view</returns>
        // GET: Ticket/Edit/5
        [Authorize(Roles = "Admin, ITStaff")]
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
                return RedirectToActionPermanent("Closed", new { id = ticket.TicketId });
            }
            EditTicketViewModel model = new EditTicketViewModel
            {
                Id = ticket.TicketId,
                Priority = ticket.Priority,
                Status = ticket.Status,
                IncidentSolution = ticket.IncidentSolution,
                DueDate = ticket.DueDate,
                ResultionDate = ticket.ResultionDate,
            };
            return View(model);
        }


        /// <summary>
        /// This action allows the Admin and IT staff to edit a ticket's details.
        /// </summary>
        /// <param name="id">Edit takes the ticket ID as a parameter</param>
        /// <returns>index,view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ITStaff")]
        public ActionResult Edit(int id, EditTicketViewModel model)
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
                ticket.Status = TicketStatus.InProgress;
                ticket.ResultionDate = model.ResultionDate;

                if (model.Priority == TicketPriority.Critical)
                {
                    ticket.DueDate = AddBusinessDays(ticket.CreationDate, 1);
                }
                if (model.Priority == TicketPriority.High)
                {
                    ticket.DueDate = AddBusinessDays(ticket.CreationDate, 3);
                }
                if (model.Priority == TicketPriority.Medium)
                {
                    ticket.DueDate = AddBusinessDays(ticket.CreationDate, 5);
                }
                if (model.Priority == TicketPriority.Low)
                {
                    ticket.DueDate = AddBusinessDays(ticket.CreationDate, 7);
                }
                if (!(model.Priority == TicketPriority.NotSet))
                {
                    ticket.Status = TicketStatus.InProgress;
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
        /// This static method is used to add a specified number of working days to a given day.
        /// </summary>
        /// <param name="current">The specified date</param>
        /// <param name="days">The number of days to be added</param>
        /// <returns></returns>
        public static DateTime AddBusinessDays(DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                }
                while (current.DayOfWeek == DayOfWeek.Friday ||
                    current.DayOfWeek == DayOfWeek.Saturday);
            }
            return current;
        }

        /// <summary>
        /// This action informs IT staff that ticket in closed and cannot be edited.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter</param>
        /// <returns></returns>
        public ActionResult Closed(int? id)
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
            var model = new EditTicketViewModel
            {
                Id = ticket.TicketId,
                ResultionDate = ticket.ResultionDate,
            };

            return View(model);
        }


        /// <summary>
        /// This action informs staff that ticket has been accelerated and connot be accelerated again.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter</param>
        /// <returns></returns>
        public ActionResult Accelerated(int? id)
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
            var model = new AccelerateTicketViewModel
            {
                Id = ticket.TicketId,
                AccelerationDate = ticket.AccelerationDate,
            };
            return View(model);
        }

        /// <summary>
        /// This action will displpay the ticket information.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter.</param>
        /// <returns>Ticket info partial view</returns>
        public PartialViewResult TicketInfoPartial(int? id)
        {
            Ticket ticket = db.Tickets.Find(id);
            TicketViewModel model = new TicketViewModel
            {
                Id = ticket.TicketId,
                Subject = ticket.Subject,
                IncidentDescription = ticket.IncidentDescription,
                CreationDate = ticket.CreationDate,
                Category = ticket.Category.CategoryName,
                CreatedByName = ticket.Employee.FullName,
                TicketOwnerName = ticket.StaffOwner.FullName,
                DueDate = ticket.DueDate,
                importance = ticket.StaffOwner.ManagerialPosition.ToString(),
            };
            return PartialView(model);
        }

        /// <summary>
        /// This action will displpay the IT staff responsible information.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter.</param>
        /// <returns>Ticket info partial view</returns>
        public PartialViewResult ITStaffInfoPartial(int? id)
        {
            Ticket ticket = db.Tickets.Find(id);
            TicketViewModel model = new TicketViewModel
            {
                Id = ticket.TicketId,
                Subject = ticket.Subject,
                ITStaffResponsibleName = ticket.Category.ITStaff.FullName,
                ITstaffEmail = ticket.Category.ITStaff.Email,
                ITstaffMobile = ticket.Category.ITStaff.Mobile,
                ITstaffExt = ticket.Category.ITStaff.ExtensionNumber,
            };
            return PartialView(model);
        }

        /// <summary>
        /// This action allows staff to accelerate tickets.
        /// </summary>
        /// <param name="id">Ticket Id as a parameter.</param>
        /// <returns>Ticket info partial view</returns>
        [Authorize(Roles = "Staff")]
        public ActionResult Accelerate(int? id)
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

            if (ticket.Accelerated == true)
            {
                return RedirectToActionPermanent("Accelerated", new { id = ticket.TicketId });
            }

            AccelerateTicketViewModel model = new AccelerateTicketViewModel
            {
                Id = ticket.TicketId,
                Accelerated = ticket.Accelerated,
                AccelerationComment = ticket.AccelerationComment,
                AccelerationDate = DateTime.Now,
            };
            return View(model);
        }


        /// <summary>
        /// This action allows Staff to accelerate tickets.
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <param name="model">AccelerateTicketViewModel.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public ActionResult Accelerate(int id, AccelerateTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                ticket.AccelerationComment = model.AccelerationComment;
                ticket.Accelerated = true;
                ticket.AccelerationDate = DateTime.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }



        public async static Task SendEmail(string recipientmail, string subject, string message1)
        {
            try
            {

                var _email = "ihelpdesk23@gmail.com";
                var _password = ConfigurationManager.AppSettings["EmailPassword"];
                var _displayName = "IT Help Desk Ticket";
                MailMessage sysMessage = new MailMessage();
                sysMessage.To.Add(_email);
                sysMessage.From = new MailAddress(_email, _displayName);
                sysMessage.Subject = subject;
                sysMessage.Body = message1;
                sysMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(sysMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
