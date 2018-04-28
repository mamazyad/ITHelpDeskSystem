/*
* Description: This file is the ticket ViewModel (based on the ticket model), created to to pass information between ticket views and its controller.
* Author: mamazyad
*/

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
    /// <summary>
    /// Ticket view model from the ticket model and used by ticket controller.
    /// </summary>
    public class TicketViewModel
    {
        public TicketViewModel()
        {
            Assignments = new List<Assignment>();
            Comments = new List<Comment>();
        }
        public int Id { get; set; }

        //Attributes of Creation//

        [Required]
        [StringLength(128, ErrorMessage = "The subject length exceeds the limit allowed.")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Incident Description")]
        [DataType(DataType.MultilineText)]
        public string IncidentDescription { get; set; }

        public HttpPostedFileBase Attachment { get; set; }

        public string AttachmentFilePath { get; set; }

        [Display(Name = "Creation date")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}")]
        public DateTime CreationDate { get; set; }

        public int? CreatedBy { get; set; }

        [Display(Name = "Created by")]
        public string CreatedByName { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int? TicketOwner { get; set; }

        public string Staff { get; set; }

        [Display(Name = "Owner")]
        public string TicketOwnerName { get; set; }

        public int? ITStaffResponsible { get; set; }

        [Display(Name = "IT Staff Responsible")]
        public string ITStaffResponsibleName { get; set; }

        [Display(Name = "Staff managerial level")]
        public string importance { get; set; }

        [Display(Name = "Email")]
        public string ITstaffEmail { get; set; }

        [Display(Name = "Mobile")]
        public string ITstaffMobile { get; set; }

        [Display(Name = "Ext.")]
        public string ITstaffExt { get; set; }

        //Attributes of Editing//

        [Display(Name = "Ticket Priority")]
        [DefaultValue(TicketPriority.Medium)]
        public TicketPriority? Priority { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yy hh:mm tt}")]
        public DateTime? DueDate { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}")]
        [Display(Name = "Resultion Date")]
        public DateTime? ResultionDate { get; set; }

        [Display(Name = "Incident Solution")]
        [DataType(DataType.MultilineText)]
        public string IncidentSolution { get; set; }


        //Attributes of Acceleration//

        //[Display(Name = "Accelarated By")]
        //public int? AccelaratedBy { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Acceleration Date")]
        //public DateTime? AccelerationDate { get; set; }

        //[Display(Name = "Acceleration Comment")]
        //[DataType(DataType.MultilineText)]
        //public string AccelerationComment { get; set; }

        public List<Assignment> Assignments { get; set; }

        public Employee Employee { get; set; }

        public List<Comment> Comments { get; set; }
    }
}