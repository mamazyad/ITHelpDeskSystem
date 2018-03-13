/*
* Description: This application is a web-based incident tracking system for IT Help Desks. 
* Author: mamazyad
* Due date: 27/02/2018
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            Assignments = new HashSet<Assignment>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int TicketId { get; set; }

        [StringLength(128)]
        public string Subject { get; set; }

        public string IncidentDescription { get; set; }

        public TicketPriority Priority { get; set; }

        public TicketStatus Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ResultionDate { get; set; }

        public string IncidentSolution { get; set; }

        public int CategoryId { get; set; }

        public int CreatedBy { get; set; }

        public int TicketOwner { get; set; }

        public int? AccelaratedBy { get; set; }

        public DateTime? AccelerationDate { get; set; }

        public string AccelerationComment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Category Category { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Staff Accelerator { get; set; }

        public virtual Staff StaffOwner { get; set; }
    }

    public enum TicketPriority
    {
        Hight,

        Medium,

        Low,
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
