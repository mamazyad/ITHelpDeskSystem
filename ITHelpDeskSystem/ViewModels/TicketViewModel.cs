using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Incident Description")]
        public string IncidentDescription { get; set; }

        [Display(Name = "Ticket Priority")]
        public TicketPriority? Priority { get; set; }

        [Display(Name = "Ticket Status")]
        public TicketStatus? Status { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Resultion Date")]
        public DateTime? ResultionDate { get; set; }

        [Display(Name = "Incident Solution")]
        public string IncidentSolution { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        [Display(Name = "Owner")]
        public int TicketOwner { get; set; }

        [Display(Name = "Accelarated By")]
        public int AccelaratedBy { get; set; }

        [Display(Name = "Acceleration Date")]
        public DateTime? AccelerationDate { get; set; }

        [Display(Name = "Acceleration Comment")]
        public string AccelerationComment { get; set; }
    }
}