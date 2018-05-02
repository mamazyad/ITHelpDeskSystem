/*
* Description: This file contains the domain of the employees, created as the super-class of the system users and storing thier common attributes.
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
    /// Employee class pertains to all the employees in a company using the IT Help Desk system.
    /// </summary>

    [Table("Employee")]
    public partial class Employee : ApplicationUser
    {
        public Employee()
        {
            Tickets = new HashSet<Ticket>();
            Comments = new HashSet<Comment>();
        }

        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(128)]
        public string JobTitle { get; set; }

        [StringLength(128)]
        public string Department { get; set; }

        [StringLength(128)]
        public string ExtensionNumber { get; set; }

        [StringLength(128)]
        public string Mobile { get; set; }

        [StringLength(128)]
        public string OfficeNumber { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
