/*
* Description: This file contains the domain of the IT Help desk system admin, created to store the admin information.
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
    /// IT Help Desk Admin is the class that obtain the information related to the system admin.
    /// </summary>

    [Table("ITHelpDeskAdmin")]
    public partial class ITHelpDeskAdmin : ITStaff
    {
        [StringLength(128)]
        public string Degree { get; set; }
    }
}
