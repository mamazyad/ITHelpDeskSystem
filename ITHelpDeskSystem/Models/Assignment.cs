/*
* Description: This file contains the domain for the assignment, created to keep track of the date in which IT Staff is assigned/reassigned to a ticket with its comments.
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
        public string AssignedTo { get; set; }

        public int? AssignedBy { get; set; }

        public string AssignedByName { get; set; }

        public string AssignmentComment { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual Category Category { get; set; }
    }
}
