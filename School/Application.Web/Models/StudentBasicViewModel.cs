namespace Application.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class StudentBasicViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Име на студент")]
        public string Name { get; set; }

        public string UserName { get; set; }
    }
}