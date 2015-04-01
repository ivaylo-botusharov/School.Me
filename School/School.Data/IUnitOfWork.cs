namespace School.Data
{
    using School.Data.Repositories;

    public interface IUnitOfWork
    {
        IApplicationUserRepository Users { get; }

        IStudentRepository Students { get; }

        ITeacherRepository Teachers { get; }

        IAdministratorRepository Administrators { get; }

        ISchoolClassRepository SchoolClasses { get; }

        IAcademicYearRepository AcademicYears { get; }

        ISchoolThemeRepository SchoolThemes { get; }

        IGradeRepository Grades { get; }

        ISubjectRepository Subjects { get; }

        ILessonRepository Lessons { get; }

        IHomeworkRepository Homeworks { get; }

        IAttachmentRepository Attachments { get; }

        ILessonAttachmentRepository LessonAttachments { get; }

        IHomeworkAttachmentRepository HomeworkAttachments { get; }

        IHomeworkSolutionRepository HomeworkSolutions { get; }

        ITotalScoreRepository TotalScores { get; }

        IMasterScheduleRepository MasterSchedules { get; }

        ApplicationDbContext Context { get; }

        void Save();
    }
}