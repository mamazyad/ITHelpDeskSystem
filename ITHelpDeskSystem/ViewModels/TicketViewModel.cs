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
        [DataType(DataType.MultilineText)]
        public string IncidentDescription { get; set; }

        [Display(Name = "Ticket Priority")]
        public TicketPriority? Priority { get; set; }

        [Display(Name = "Ticket Status")]
        public TicketStatus? Status { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Resultion Date")]
        public DateTime? ResultionDate { get; set; }

        [Display(Name = "Incident Solution")]
        [DataType(DataType.MultilineText)]
        public string IncidentSolution { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        [Display(Name = "Owner")]
        public int TicketOwner { get; set; }

        [Display(Name = "Accelarated By")]
        public int AccelaratedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Acceleration Date")]
        public DateTime? AccelerationDate { get; set; }

        [Display(Name = "Acceleration Comment")]
        [DataType(DataType.MultilineText)]
        public string AccelerationComment { get; set; }
    }
}