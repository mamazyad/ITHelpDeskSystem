/*
* Description: This file contains the domain of the criteria, created to hold the targeted grades Staff are evaluated on.
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

        public string CriterionName { get; set; }

        public string CriterionDescription { get; set; }

        public string TargetGrade { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? EditionDate { get; set; }

        public bool ActiveCriterion { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
