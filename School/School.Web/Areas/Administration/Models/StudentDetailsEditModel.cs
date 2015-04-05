namespace School.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentDetailsEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The name must not be more than 100 characters.")]
        public string Name { get; set; }

        [Range(5, 100)]
        public int? Age { get; set; }

        public AccountDetailsEditModel AccountDetailsEditModel { get; set; }
    }
}