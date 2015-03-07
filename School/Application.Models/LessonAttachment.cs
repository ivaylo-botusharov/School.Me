using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class LessonAttachment
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        public virtual Attachment Attachment { get; set; }

        public int AttachmentId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
