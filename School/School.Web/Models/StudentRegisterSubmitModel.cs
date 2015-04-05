namespace School.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using School.Web.Models.AccountViewModels;

    public class StudentRegisterSubmitModel
    {
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not be longer than {1} symbols.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; }
    }
}