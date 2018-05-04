/*
* Description: This file is the edit ticket ViewModel (based on the ticket model), created to to pass information related to editing a ticket.
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
    /// Edit ticket view model from the ticket model and used by the edit ticket controller.
    /// </summary>
    public class EditTicketViewModel 
    {
       public int Id { get; set; }

        [Required]
        [Display(Name = "Ticket Priority")]
        public TicketPriority? Priority { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}")]
        public DateTime? DueDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}")]
        [Display(Name = "Resultion Date")]
        public DateTime? ResultionDate { get; set; }

        [Display(Name = "Incident Solution")]
        [DataType(DataType.MultilineText)]
        public string IncidentSolution { get; set; }
    }
}