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

        private ICollection<Subject> subjects;

        private ICollection<Homework> homeworks;

        public SchoolClass()
        {
            this.students = new List<Student>();
            this.subjects = new HashSet<Subject>();
            this.homeworks = new HashSet<Homework>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        //public virtual Grade Grade { get; set; }

        
        //public int GradeId { get; set; }

        public virtual List<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Subject> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
