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

        [Display(Name = "Ticket Status")]
        public TicketStatus? Status { get; set; }

        //[Display(Name = "Due Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? DueDate { get; set; }

        public DateTime? CreationDate { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Resultion Date")]
        //public DateTime? ResultionDate { get; set; }

        //[Display(Name = "Incident Solution")]
        //[DataType(DataType.MultilineText)]
        //public string IncidentSolution { get; set; }

        [Display(Name = "Created By")]
        public int? CreatedBy { get; set; }

        [Display(Name = "Created by")]
        public string CreatedByName { get; set; }

        public string TicketOwnerName { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public int TicketOwner { get; set; }

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

        public int? StaffId { get; set; }

       public string Staff { get; set; }

    }
}