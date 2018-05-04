/*
* Description: This file is the knowledge base ViewModel (based on the knowledge base model), created to to pass information between knowledge base views and its controller.
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
    /// Knowdledge Base view Model based on knowedge base model and used by knowedge base controller.
    /// </summary>
    public class KnowledgeBaseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        [Display(Name = "Incident Title")]
        public string IncidentTitle { get; set; }

        [Required]
        [Display(Name = "Incident Description")]
        [DataType(DataType.MultilineText)]
        public string IncidentDescription { get; set; }

        [Required]
        [Display(Name = "Solution Description")]
        [DataType(DataType.MultilineText)]
        public string SolutionDescription { get; set; }

        public string KBAttachmentFilePath { get; set; }

        [Display(Name = "Attachment")]
        public HttpPostedFileBase KBAttachment { get; set; }

        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Updated On")]
        [DisplayFormat(DataFormatString = "{0:dd / MM - hh:mm}", NullDisplayText = "Not updated")]
        public DateTime? EditionDate { get; set; }

        public int? EditedBy { get; set; }

        [Display(Name = "Updated By")]
        [DisplayFormat(NullDisplayText = "Not updated")]
        public string EditedByName { get; set; }

        public int CreatedBy { get; set; }

        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }

        public int ITStaffId { get; set; }

        public virtual ITStaff ITStaff { get; set; }

        public string Search { get; set; }
    }
}