/*
* Description: This file is the ticket ViewModel (based on the ticket model), created to to pass information between ticket views and its controller.
* Author: mamazyad
* Date: 20/03/2018
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
        
        public int Id { get; set; }

        //Creation//

        [Required]
        [StringLength(128, ErrorMessage = "The subject length exceeds the limit allowed.")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Incident Description")]
        [DataType(DataType.MultilineText)]
        public string IncidentDescription { get; set; }

        public HttpPostedFileBase Attachment { get; set; }

        public string AttachmentFilePath { get; set; }

        [Display (Name = "Creation date")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
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



        //Editing//

        [Display(Name = "Ticket Priority")]
        [DefaultValue(TicketPriority.Medium)]
        public TicketPriority? Priority { get; set; }

        public TicketStatus? Status { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Resultion Date")]
        public DateTime? ResultionDate { get; set; }

        [Display(Name = "Incident Solution")]
        [DataType(DataType.MultilineText)]
        public string IncidentSolution { get; set; }


        //Acceleration//

        //[Display(Name = "Accelarated By")]
        //public int? AccelaratedBy { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Acceleration Date")]
        //public DateTime? AccelerationDate { get; set; }

        //[Display(Name = "Acceleration Comment")]
        //[DataType(DataType.MultilineText)]
        //public string AccelerationComment { get; set; }
    }
}