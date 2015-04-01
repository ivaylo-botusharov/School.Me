namespace School.Models
{
    using System;

    public class MasterSchedule : DeletableEntity
    {
        public int Id { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }

        public Guid SchoolClassId { get; set; }

        public virtual Subject Subject { get; set; }

        public int? SubjectId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public Guid TeacherId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public int LessonId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ClassRoomNumber { get; set; }
    }
}