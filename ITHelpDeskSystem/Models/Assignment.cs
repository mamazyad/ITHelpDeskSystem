/*
* Description: This file contains the domain for the assignment, created to keep track of the date in which IT Staff is assigned/reassigned to a ticket with its comments.
* Author: mamazyad
* Date: 20/03/2018
*/

namespace ITHelpDeskSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Assignment class will keep track of the date an IT Staff is assigned to a ticket and any associated comments.
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
