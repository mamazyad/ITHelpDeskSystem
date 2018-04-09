/*
* Description: This file contains the domain of the comments, created to store Staff's and ITstaff's comments on the Iticket.
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

    public class Comment
    {
        public int CommentId { get; set; }

        public DateTime CommentDate { get; set; }

        public string CommentText { get; set; }

        public string Title { get; set; }

        public string Commenter { get; set; }

        public string UpdatedCommentText { get; set; }

        public DateTime? EditionDate { get; set; }

        public int TicketId { get; set; }

        public int? StaffId { get; set; }

        public int? ITStaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public virtual Ticket Ticket { get; set; }

    }
}