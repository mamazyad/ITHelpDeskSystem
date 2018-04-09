/*
* Description: This file contains the domain of the categories, created to identify the various IT categories IT staff are responsible for.
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
    ///  Category class contains the fields each IT staff is responsible for.
    /// </summary>
    
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Tickets = new HashSet<Ticket>();
            Assignments = new HashSet<Assignment>();
        }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(128)]
        [Index(IsUnique = true)]
        public string CategoryName { get; set; }

        [StringLength(512)]
        public string CategoryDescription { get; set; }

        public int ITStaffId { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
