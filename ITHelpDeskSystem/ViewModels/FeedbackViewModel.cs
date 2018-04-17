/*
* Description: This file is the feedback view model (based on the feedback model), created to to pass information between feedback views and its controller.
* Author: mamazyad
*/

using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    /// <summary>
    /// Feedback view model based on feedback model and used by feedback controller.
    /// </summary>
    public class FeedbackViewModel
    {
        public FeedbackViewModel()
        {
            Criteria = new List<CriterionViewModel>();
        }
        public int Id { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public string FeedbackComment { get; set; }

        public int TicketId { get; set; }

        public List<CriterionViewModel> Criteria { get; set; }
    }
}