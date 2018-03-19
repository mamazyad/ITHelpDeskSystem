/*
* Description: This file is the domain of the feedbacks, created to obtain Staff's feedback on the IT staff performance.
* Author: mamazyad
*/

namespace ITHelpDeskSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Feedback class contains the evaluation of the IT Staff on the services provided as submitted by Staff.
    /// </summary>

    [Table("Feedback")]
    public partial class Feedback
    {
        public int FeedbackId { get; set; }

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
