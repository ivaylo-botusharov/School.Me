using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Lesson
    {
        private ICollection<LessonAttachment> lessonAttachments; 

        public Lesson()
        {
            this.lessonAttachments = new HashSet<LessonAttachment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Homework Homework { get; set; }

        public int HomeworkId { get; set; }

        public virtual Subject Subject { get; set; }

        public int? SubjectId { get; set; }

        public virtual ICollection<LessonAttachment> LessonAttachments
        {
            get { return this.lessonAttachments; }
            set { this.lessonAttachments = value; }
        }
    }
}