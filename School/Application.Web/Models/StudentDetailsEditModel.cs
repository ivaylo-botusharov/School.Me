namespace Application.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class StudentDetailsEditModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The name must not be more than 100 characters.")]
        public string Name { get; set; }

        [Range(5, 100)]
        public int? Age { get; set; }

        public string UserName { get; set; }
    }
}