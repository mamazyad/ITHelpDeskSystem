/*
* Description: This file is the feedback view model (based on the feedback model), created to to pass information between feedback views and its controller.
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
    public class FeedbackViewModel
    {
        public int Id { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public string FeedbackComment { get; set; }

        [StringLength(64)]
        public string GradeGiven { get; set; }

        public int TicketId { get; set; }

        public int StaffId { get; set; }

        public int CriterionId { get; set; }

        public virtual Criterion Criterion { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}