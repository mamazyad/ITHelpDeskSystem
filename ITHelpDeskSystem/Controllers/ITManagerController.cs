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

        [Authorize(Roles = "ITManager")]
        public ActionResult ITManagerHome()
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


        // GET: ITManager
        [Authorize(Roles = "ITManager")]
        public ActionResult Index()
        {
            var ITstaffs = db.ITStaffs.ToList();
            var Tickets = db.Tickets.ToList();
            var model = new List<ITStaffViewModel>();
            foreach (var item in ITstaffs)
            {
                var cat = db.Categories.Where(m => m.ITStaffId == item.Id).ToList();
                foreach (var itemx in cat)
                {
                    model.Add(new ITStaffViewModel
                    {
                        Id = item.Id,
                        FirstName = item.FullName,
                        TicketLoad = db.Tickets.Count(m => m.CategoryId == itemx.CategoryId),
                });
                }
            }
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

        // GET: ITManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ITManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ITManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ITManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ITManager/Edit/5
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

        // GET: ITManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ITManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
