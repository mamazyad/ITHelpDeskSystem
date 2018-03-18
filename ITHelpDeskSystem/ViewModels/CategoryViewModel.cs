using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
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