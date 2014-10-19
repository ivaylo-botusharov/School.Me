namespace Application.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

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
        public string PhoneNumber { get; set; }
    }
}