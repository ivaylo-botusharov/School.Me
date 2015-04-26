namespace School.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AccountDetailsEditModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}