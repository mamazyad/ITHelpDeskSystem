using ITHelpDeskSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITHelpDeskSystem.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}