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
    public class AccelerateTicketViewModel
    {
        public int Id { get; set; }

        public bool Accelerated { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}", NullDisplayText = "Not set")]
        [Display(Name = "Acceleration Date")]
        public DateTime? AccelerationDate { get; set; }

        [Display(Name = "Acceleration Comment")]
        [DataType(DataType.MultilineText)]
        public string AccelerationComment { get; set; }
    }
}