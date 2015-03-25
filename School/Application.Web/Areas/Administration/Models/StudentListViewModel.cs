using Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models
{
    public class StudentListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}