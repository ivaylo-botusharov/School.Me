namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TeacherListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string Name { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}