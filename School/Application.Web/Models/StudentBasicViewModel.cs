using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Application.Web.Models
{
    public class StudentBasicViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име на студент")]
        public string Name { get; set; }
    }
}