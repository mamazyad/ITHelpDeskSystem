/*
* Description: This file contains the charts controller that will allow the creation of the system's charts.
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
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Charts
        public ActionResult Index()
        {
            // This chart displays on
            // x-axes: Department names that contain one or more faculties
            // y-axes: Number of faculties in each department

            var TotalTickets = db.Tickets.Where(m => m.Status == TicketStatus.Open).ToList();
            var ClosedTickets = db.Tickets.Where(m=>m.Status == TicketStatus.Closed).ToList();
            int count = 0;
            var labels = new List<string>();
            var data = new List<int>();
            foreach (var item in TotalTickets)
            {
                // Find the number of faculties in the current department
                count = ClosedTickets.Count();
                if (count != 0)
                {
                    labels.Add(item.IncidentDescription);
                    data.Add(count);
                }
            }

            ViewBag.Labels = labels.ToArray();
            ViewBag.Data = data.ToArray();

            return View();
        }

    }
    }

