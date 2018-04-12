/*
* Description: This file is the criterion ViewModel (based on the criterion model), created to to pass information between criterion views and its controller.
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
    /// Criterion view model based on the criterion model and used by the criterion controller.
    /// </summary>

    public class CriterionViewModel
    {
        public CriterionViewModel()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }

        [Display(Name = "Criterion Name")]
        public string CriterionName { get; set; }

        [Display(Name = "Criterion Description")]
        [DataType(DataType.MultilineText)]
        public string CriterionDescription { get; set; }

        //[Display(Name = "Target Grade")]
        public string PossibleAnswers { get; set; }

        public string Text { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Updated on")]
        public DateTime? EditionDate { get; set; }

        public int? SelectedAnswer { get; set; }

        [Display(Name = "Active Criterion")]
        public bool ActiveCriterion { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public List<AnswerViewModel> PossibeAnswers { get; set; }
    }
}