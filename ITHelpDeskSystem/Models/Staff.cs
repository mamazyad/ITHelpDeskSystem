/*
* Description: This application is a web-based incident tracking system for IT Help Desks. 
* Author: mamazyad
* Due date: 27/02/2018
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ITHelpDeskSystem.Models
{
    /// <summary>
    /// Staff is the class pertaining to the company's employees that are not in the IT department. These are the staff in need of technical support.
    /// </summary>

    [Table("Staff")]
    public partial class Staff : Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Feedbacks = new HashSet<Feedback>();
            TicketsAccelarated = new HashSet<Ticket>();
            TicketsCreated = new HashSet<Ticket>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffId { get; set; }

        [StringLength(128)]
        public string StaffLevel { get; set; }

        public ManagerialPosition? ManagerialPosition { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> TicketsAccelarated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> TicketsCreated { get; set; }
    }

    /// <summary>
    /// The Managerial Position will signify the priority of the ticket created by that particular Staff.
    /// </summary>
    public enum ManagerialPosition
    {
        High,
        
        Regular,
    }
}
