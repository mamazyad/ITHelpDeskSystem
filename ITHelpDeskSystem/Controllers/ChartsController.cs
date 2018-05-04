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

            var itstaffs = db.ITStaffs.ToList();
            var feedback = db.Feedbacks.Select(p=>p.Grade).Sum();
            decimal? count = 0;
            var labels = new List<string>();
            var data = new List<decimal?>();
            foreach (var item in itstaffs)
            {
                // Find the number of faculties in the current department
                count = feedback;
                if (count != 0)
                {
                    labels.Add(item.FullName);
                    data.Add(count);
                }
            }

            // Convert labels and data from lists to arrays and save them in ViewBag
            ViewBag.Labels = labels.ToArray();
            ViewBag.Data = data.ToArray();
            return View();
        }

    }
    }

