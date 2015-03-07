using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Teacher : DeletableEntity
    {
        private ICollection<Subject> subjects;

        public Teacher()
        {
            this.subjects = new HashSet<Subject>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<Subject> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }
    }
}
