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
    /// Criterion class is the basis on which ticket feedback is provided.
    /// </summary>

    [Table("Criterion")]
    public partial class Criterion
    {
        public Criterion()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int CriterionId { get; set; }

        [StringLength(128)]
        public string CriterionName { get; set; }

        [StringLength(256)]
        public string CriterionDescription { get; set; }

        [StringLength(64)]
        public string TargetGrade { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
