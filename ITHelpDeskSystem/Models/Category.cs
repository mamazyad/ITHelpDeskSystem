/*
* Description: This application is a web-based incident tracking system for IT Help Desks. 
* Author: mamazyad
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
        }

        public int CategoryId { get; set; }

        [StringLength(128)]
        public string CategoryName { get; set; }

        [StringLength(512)]
        public string CategoryDescription { get; set; }

        public int? ITStaffId { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
