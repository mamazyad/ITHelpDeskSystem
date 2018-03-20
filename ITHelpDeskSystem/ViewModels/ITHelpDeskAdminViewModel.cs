/*
* Description: This file is the IT help desk admin ViewModel (based on the IT help desk admin  model), created to to pass information between IT help desk admin views and its controller.
* Author: mamazyad
* Date: 20/03/2018
*/
using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    /// <summary>
    /// ITSatff view model from the IT staff model and used by ITStaff controller.
    /// </summary>

    public class ITHelpDeskAdminViewModel
    {
        public ITHelpDeskAdminViewModel()
        {
            Categories = new List<Category>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Department { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        public string Mobile { get; set; }

        [Display(Name = "Extension Number")]
        public string ExtensionNumber { get; set; }

        [Display(Name = "Office Number")]
        public string OfficeNumber { get; set; }

        public string Speciality { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Starting Date")]
        public DateTime? StartingDate { get; set; }

        public string Position { get; set; }

        public string Degree { get; set; }

        public string Roles { get; set; }

        public List<Category> Categories { get; set; }
    }
}