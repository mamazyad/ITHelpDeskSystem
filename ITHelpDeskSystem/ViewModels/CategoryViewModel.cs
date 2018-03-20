/*
* Description: This file is the categories ViewModel (based on the categories model), created to to pass information between category views and its controller.
* Author: mamazyad
* Date: 20/03/2018
*/

using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    /// <summary>
    /// Category view model based on the Category model and used by the Category controller.
    /// </summary>
    
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Tickets = new List<Ticket>();
        }
        public int Id { get; set; }


        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        public int ITStaffId { get; set; }

        [Display(Name = "IT Staff")]
        public string ITStaff { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}