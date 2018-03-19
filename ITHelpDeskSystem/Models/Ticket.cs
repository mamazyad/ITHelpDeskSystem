/*
* Description: Rhis file is the domain of the tickets, the central and most vital model in the IT help desk system. It is created to obtain ticket information, containing enums to define the ticket status and priority.
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
    /// Ticket is the class that provide information regarding the Staff request for help.
    /// </summary>

    [Table("Ticket")]
    public partial class Ticket
    {
        public Ticket()
        {
            Assignments = new HashSet<Assignment>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int TicketId { get; set; }

        [Required]
        [StringLength(128)]
        public string Subject { get; set; }

        [Required]
        public string IncidentDescription { get; set; }

        public TicketPriority? Priority { get; set; }

        public TicketStatus? Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ResultionDate { get; set; }

        public string IncidentSolution { get; set; }


        public int CategoryId { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public int? TicketOwner { get; set; }

        public string TicketOwnerName { get; set; }

        public int? AccelaratedBy { get; set; }

        public DateTime? AccelerationDate { get; set; }

        public string AccelerationComment { get; set; }


        public virtual Category Category { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Staff StaffOwner { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Staff Accelerator { get; set; }
    }

    public enum TicketPriority
    {
        High,

        Medium,

        Low,

        Critical,
    }

    public enum TicketStatus
    {
        Open,

        [Display(Name = "In progress")]
        InProgress,

        [Display(Name = "In progress (vendor)")]
        InProgressVendor,

        Resolved,

        Closed,
    }
}
