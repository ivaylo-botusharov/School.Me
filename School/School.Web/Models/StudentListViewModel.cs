namespace School.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StudentListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string Name { get; set; }
    }
}