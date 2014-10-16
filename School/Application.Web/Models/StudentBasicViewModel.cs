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
        public Guid Id { get; set; }

        [Display(Name = "Име на студент")]
        public string Name { get; set; }
    }
}