using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        public DateTime? AssignmentDate { get; set; }

        [Display(Name ="Comment")]
        [DataType(DataType.MultilineText)]
        public string AssignmentComment { get; set; }

        public int? AssignedBy{ get; set; }

        [Display(Name = "Assigned by")]
        public string AssignedByName { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int ITStaffId { get; set; }

        [Display(Name = "Assigned to")]
        public string ITStaff { get; set; }

        public int TicketId { get; set; }
    }
}