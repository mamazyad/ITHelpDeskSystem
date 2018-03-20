/*
* Description:This file is the domain of the Knowledge Base, created to act as the syste'ms central solutions deposotory.
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
    /// Knowledge base is the class that acts as a repository of information related to trubleshooting IT problems that all users can access.
    /// </summary>

    [Table("KnowledgeBase")]
    public partial class KnowledgeBase
    {
        public int KnowledgeBaseId { get; set; }

        [StringLength(128)]
        public string Topic { get; set; }

        [StringLength(512)]
        public string IncidentTitle { get; set; }

        public string IncidentDescription { get; set; }

        public string SolutionDescription { get; set; }

        public DateTime? CreationDate { get; set; }

        public int ITStaffId { get; set; }

        public virtual ITStaff ITStaff { get; set; }
    }
}
