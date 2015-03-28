namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AdministratorRegisterSubmitModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; }
    }
}