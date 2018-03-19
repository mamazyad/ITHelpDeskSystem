/*
* Description: This file is the domain for the assignment, created to keep track of IT Staff assigned to a ticket.
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
    /// Assignment class will keep track of the IT Staff assigned to a ticket.
    /// </summary>

    [Table("Assignment")]
    public partial class Assignment
    {
        public int AssignmentId { get; set; }

        public DateTime? AssignmentDate { get; set; }

        [StringLength(1024)]
        public string AssignmentComment { get; set; }

        public int TicketId { get; set; }

        public int ITStaffId { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
