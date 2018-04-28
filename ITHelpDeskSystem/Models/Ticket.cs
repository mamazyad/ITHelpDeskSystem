/*
* Description: This file contains the domain of the tickets, the central and most vital model in the IT help desk system. It is created to obtain ticket information, containing TicketPriority enum to define the ticket priority and TicketStatus that describes the ticket condition. 
* Author: mamazyad
*/

namespace ITHelpDeskSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using System.Net.Mail;
    using System.Net;

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
            Comments = new HashSet<Comment>();
        }

        public int TicketId { get; set; }

        [Required]
        [StringLength(128)]
        public string Subject { get; set; }

        [Required]
        public string IncidentDescription { get; set; }

        public TicketPriority? Priority { get; set; }

        public TicketStatus Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ResultionDate { get; set; }

        public string IncidentSolution { get; set; }

        public string AttachmentFilePath { get; set; }

        public int CategoryId { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public int? TicketOwner { get; set; }

        public string TicketOwnerName { get; set; }

        public int? ITStaffResponsible { get; set; }

        public string ITStaffResponsibleName { get; set; }

        public DateTime? AccelerationDate { get; set; }

        public string AccelerationComment { get; set; }

        public int? AccelaratedBy { get; set; }

        public string importance { get; set; }
              
        public string ITstaffEmail { get; set; }

        public string ITstaffMobile { get; set; }

        public string ITstaffExt { get; set; }

        public bool Accelerated { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Staff StaffOwner { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Staff Accelerator { get; set; }

        //public int? LagTime
        //{
        //    get { return Convert.ToInt32(ResultionDate - DueDate); }
        //}
        //public TimeSpan? LagTime { get; set; }
    }

    /// <summary>
    /// The ticket priority defines the importance of the ticket, critical being the most important and low is the least important.
    /// </summary>
    public enum TicketPriority
    {
        [Display(Name = "Not set")]
        NotSet,

        Critical,

        High,

        Medium,

        Low,
    }

    /// <summary>
    /// The ticket status defines the progress of the ticket, open: ticket is assigned to IT staff, in-progress: ticket is in the process of being investigated and resolved, resolved: a solution has been put in place but has not yet been validated by Staff, closed: staff has agreed that the incident has been solved and gave feedback on the service received.
    /// </summary>
    public enum TicketStatus
    {
        Open,

        [Display(Name = "In progress")]
        InProgress,

        Resolved,

        Closed,
    }
  
}