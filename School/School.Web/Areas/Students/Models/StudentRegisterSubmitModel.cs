namespace School.Web.Areas.Students.Models
{
    using System.ComponentModel.DataAnnotations;
    using School.Web.Areas.Students.Models.AccountViewModels;
    
    public class StudentRegisterSubmitModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not be longer than {1} symbols.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; }
    }
}