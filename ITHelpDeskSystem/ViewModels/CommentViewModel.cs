/*
* Description: This file is the comments ViewModel (based on the comments model), created to to pass information between comments views and its controller.
* Author: mamazyad
*/

using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    /// <summary>
    /// Comment view model based on the comment model and used by the comment controller.
    /// </summary>

    public class CommentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Comment Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, MMM dd - hh:mm tt}", NullDisplayText = "Not set")]
        public DateTime CommentDate { get; set; }

        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string CommentText { get; set; }

        public string Commenter { get; set; }

        public string Title { get; set; }

        [Display(Name = "Update")]
        [DataType(DataType.MultilineText)]
        public string UpdatedCommentText { get; set; }

        [Display(Name = "Updated on")]
        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}", NullDisplayText = "Not set")]
        public DateTime? EditionDate { get; set; }

        public int TicketId { get; set; }

        public int? StaffId { get; set; }

        public int? ITStaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}