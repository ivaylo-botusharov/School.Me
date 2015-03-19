using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SchoolClass
    {
        private List<Student> students;

        public SchoolClass()
        {
            this.students = new List<Student>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public virtual Grade Grade { get; set; }

        public int GradeId { get; set; }

        public virtual SchoolTheme SchoolTheme { get; set; }

        public int SchoolThemeId { get; set; }

        public virtual List<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }
    }
}
