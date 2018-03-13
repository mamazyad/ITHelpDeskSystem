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
            Categories = new List<Category>();
        }

        public int Id { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
        
        public List<Category> Categories { get; set; }
    }
}