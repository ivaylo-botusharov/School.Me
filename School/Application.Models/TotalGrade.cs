using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TotalGrade
    {
        public int Id { get; set; }

        public int HomeworkGrade { get; set; }

        public virtual Subject Subject { get; set; }

        public int SubjectId { get; set; }

        public virtual Student Student { get; set; }

        public Guid StudentId { get; set; }
    }
}
