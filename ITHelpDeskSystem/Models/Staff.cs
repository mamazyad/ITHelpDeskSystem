/*
* Description: This file contains the domain of the staff, created to store staffs' information along side a ManagerialPosition enum to define the managerial position of a staff.
* Author: mamazyad
* Date: 20/03/2018
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
        public Staff()
        {
            Feedbacks = new HashSet<Feedback>();
            TicketsAccelarated = new HashSet<Ticket>();
            TicketsCreated = new HashSet<Ticket>();
            Comments = new HashSet<Comment>();
        }

        [StringLength(128)]
        public string StaffLevel { get; set; }

        public ManagerialPosition? ManagerialPosition { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<Ticket> TicketsAccelarated { get; set; }

        public virtual ICollection<Ticket> TicketsCreated { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

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
