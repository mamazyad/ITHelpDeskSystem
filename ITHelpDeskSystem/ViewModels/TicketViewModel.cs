using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    public class TicketViewModel
    {
        //public TicketViewModel()
        ///{
        //    CreationDate = DateTime.Now;
        //}

        public int Id { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Subject can contain 128 characters only.")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Incident Description")]
        [DataType(DataType.MultilineText)]
        public string IncidentDescription { get; set; }

        //[Display(Name = "Ticket Priority")]
        //[DefaultValue(TicketPriority.Medium)]
        //public TicketPriority Priority { get; set; }

        //[Display(Name = "Ticket Status")]
        //public TicketStatus? Status { get; set; }

        //[Display(Name = "Due Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? DueDate { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[DataType(DataType.DateTime)]
        //[Display(Name = "Creation Date")]
        //public DateTime? CreationDate { get; set; }

        private DateTime? creationDate = DateTime.Now;
        public DateTime? CreationDate { get { return creationDate; } set { creationDate = value; } }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Resultion Date")]
        //public DateTime? ResultionDate { get; set; }

        //[Display(Name = "Incident Solution")]
        //[DataType(DataType.MultilineText)]
        //public string IncidentSolution { get; set; }

        //[Required]
        //[Display(Name = "Created By")]
        //public int CreatedBy { get; set; }

        //[Required]
        //[Display(Name = "Owner")]
        //public int TicketOwner { get; set; }

        //[Display(Name = "Accelarated By")]
        //public int? AccelaratedBy { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Acceleration Date")]
        //public DateTime? AccelerationDate { get; set; }

        //[Display(Name = "Acceleration Comment")]
        //[DataType(DataType.MultilineText)]
        //public string AccelerationComment { get; set; }


        public int CategoryId { get; set; }

        public string Category { get; set; }

        //public int? StaffId { get; set; }

        //public string Staff { get; set; }
    }
}