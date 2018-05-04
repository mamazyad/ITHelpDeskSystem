/*
* Description: This file is the assignment ViewModel (based on the assignment model), created to to pass information between assignment views and its controller.
* Author: mamazyad
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
    /// Assignment view model from the assignment model and used by assignment controller.
    /// </summary>
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}", NullDisplayText = "Not set")]
        public DateTime? AssignmentDate { get; set; }

        public int? AssignedBy { get; set; }

        [Display(Name = "Assigned By")]
        public string AssignedByName { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string AssignmentComment { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        public int TicketId { get; set; }

        public Category Category { get; set; }

        public Ticket Ticket { get; set; }
    }
}