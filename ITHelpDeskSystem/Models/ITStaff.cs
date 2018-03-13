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
    /// IT Staff is the class that relates to the IT employees in the company who provide end user support through the IT Help Desk system.
    /// </summary>

    [Table("ITStaff")]
    public partial class ITStaff : Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITStaff()
        {
            Assignments = new HashSet<Assignment>();
            Categories = new HashSet<Category>();
            KnowledgeBases = new HashSet<KnowledgeBase>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int ITStaffId { get; set; }

        [StringLength(128)]
        public string Speciality { get; set; }

        public DateTime? StartingDate { get; set; }

        [StringLength(128)]
        public string Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ITHelpDeskAdmin ITHelpDeskAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KnowledgeBase> KnowledgeBases { get; set; }
    }
}
