using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models
{
    public class TeacherDetailsEditModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The name must not be more than 100 characters.")]
        public string Name { get; set; }

        [Range(5, 100)]
        public int? Age { get; set; }

        public AccountDetailsEditModel AccountDetailsEditModel { get; set; }
    }
}