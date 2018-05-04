/*
* Description: This file is the staff ViewModel (based on the staff model), created to to pass information between staff views and its controller.
* Author: mamazyad
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
    /// Satff view model from the staff model and used by staff controller.
    /// </summary>

    public class StaffViewModel
    {
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
        [Display(Name = "Confirm Password")]
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

        [Display(Name = "Level")]
        public string StaffLevel { get; set; }

        [Display(Name = "Managerial Position")]
        public ManagerialPosition? ManagerialPosition { get; set; }

        public string Roles { get; set; }
    }
}