/*
* Description: This file is the answer View Model, created to to pass information between criterion and feedback models.
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
    /// Answer view model from used by criterion and feedback controllers.
    /// </summary>
    public class AnswerViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}