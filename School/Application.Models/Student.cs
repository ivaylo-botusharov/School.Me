namespace Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Student : DeletableEntity
    {
        private ICollection<SchoolClass> schoolClasses;

        public Student()
        {
            this.schoolClasses = new HashSet<SchoolClass>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }
        
        public virtual ICollection<SchoolClass> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}