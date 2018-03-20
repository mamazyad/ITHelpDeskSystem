/*
* Description: This file contains the domain of the IT Staffs, created to store the IT staff information.
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
    /// IT Staff is the class that relates to the IT employees in the company who provide end user support through the IT Help Desk system.
    /// </summary>

    [Table("ITStaff")]
    public partial class ITStaff : Employee
    {
        public ITStaff()
        {
            Assignments = new HashSet<Assignment>();
            KnowledgeBases = new HashSet<KnowledgeBase>();
            Categories = new HashSet<Category>();
        }

        [StringLength(128)]
        public string Speciality { get; set; }

        public DateTime? StartingDate { get; set; }

        [StringLength(128)]
        public string Position { get; set; }

        public bool IsManager { get; set; }


        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<KnowledgeBase> KnowledgeBases { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
