namespace School.Data
{
    using School.Data.Repositories;
    using School.Models;
    using System;
    using System.Collections.Generic;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IApplicationDbContext context;
        private readonly IDictionary<Type, object> repositories;
        private bool disposed = false;

        public UnitOfWork(IApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ApplicationDbContext Context
        {
            get
            {
                return (ApplicationDbContext)this.context;
            }
        }

        public IApplicationUserRepository Users
        {
            get
            {
                var typeOfModel = typeof(ApplicationUser);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new ApplicationUserRepository(this.context));
                }
                return (IApplicationUserRepository)this.repositories[typeOfModel];
            }
        }

        public IStudentRepository Students
        {
            get
            {
                var typeOfModel = typeof(Student);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new StudentRepository(this.context));
                }
                return (IStudentRepository)this.repositories[typeOfModel];
            }
        }

        public ITeacherRepository Teachers
        {
            get
            {
                var typeOfModel = typeof(Teacher);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new TeacherRepository(this.context));
                }
                return (ITeacherRepository)this.repositories[typeOfModel];
            }
        }

        public IAdministratorRepository Administrators
        {
            get
            {
                var typeOfModel = typeof(Administrator);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new AdministratorRepository(this.context));
                }
                return (IAdministratorRepository)this.repositories[typeOfModel];
            }
        }

        public ISchoolClassRepository SchoolClasses
        {
            get
            {
                var typeOfModel = typeof(SchoolClass);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new SchoolClassRepository(this.context));
                }
                return (ISchoolClassRepository)this.repositories[typeOfModel];
            }
        }

        public IAcademicYearRepository AcademicYears
        {
            get
            {
                var typeOfModel = typeof(AcademicYear);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new AcademicYearRepository(this.context));
                }
                return (IAcademicYearRepository)this.repositories[typeOfModel];
            }
        }

        //public IGenericRepository<AcademicYear> AcademicYears
        //{
        //    get
        //    {
        //        return this.GetRepository<AcademicYear>();
        //    }
        //}

        public ISchoolThemeRepository SchoolThemes
        {
            get
            {
                var typeOfModel = typeof(SchoolTheme);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new SchoolThemeRepository(this.context));
                }
                return (ISchoolThemeRepository)this.repositories[typeOfModel];
            }
        }

        public IGradeRepository Grades
        {
            get
            {
                var typeOfModel = typeof(Grade);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new GradeRepository(this.context));
                }
                return (IGradeRepository)this.repositories[typeOfModel];
            }
        }

        public ISubjectRepository Subjects
        {
            get
            {
                var typeOfModel = typeof(Subject);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new SubjectRepository(this.context));
                }
                return (ISubjectRepository)this.repositories[typeOfModel];
            }
        }

        public ILessonRepository Lessons
        {
            get
            {
                var typeOfModel = typeof(Lesson);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new LessonRepository(this.context));
                }
                return (ILessonRepository)this.repositories[typeOfModel];
            }
        }

        public IHomeworkRepository Homeworks
        {
            get
            {
                var typeOfModel = typeof(Homework);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new HomeworkRepository(this.context));
                }
                return (IHomeworkRepository)this.repositories[typeOfModel];
            }
        }

        public IAttachmentRepository Attachments
        {
            get
            {
                var typeOfModel = typeof(Attachment);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new AttachmentRepository(this.context));
                }
                return (IAttachmentRepository)this.repositories[typeOfModel];
            }
        }

        public ILessonAttachmentRepository LessonAttachments
        {
            get
            {
                var typeOfModel = typeof(LessonAttachment);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new LessonAttachmentRepository(this.context));
                }
                return (ILessonAttachmentRepository)this.repositories[typeOfModel];
            }
        }

        public IHomeworkAttachmentRepository HomeworkAttachments
        {
            get
            {
                var typeOfModel = typeof(HomeworkAttachment);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new HomeworkAttachmentRepository(this.context));
                }
                return (IHomeworkAttachmentRepository)this.repositories[typeOfModel];
            }
        }

        public IHomeworkSolutionRepository HomeworkSolutions
        {
            get
            {
                var typeOfModel = typeof(HomeworkSolution);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new HomeworkSolutionRepository(this.context));
                }
                return (IHomeworkSolutionRepository)this.repositories[typeOfModel];
            }
        }

        public ITotalScoreRepository TotalScores
        {
            get
            {
                var typeOfModel = typeof(TotalScore);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new TotalScoreRepository(this.context));
                }
                return (ITotalScoreRepository)this.repositories[typeOfModel];
            }
        }

        public IMasterScheduleRepository MasterSchedules
        {
            get
            {
                var typeOfModel = typeof(MasterSchedule);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new MasterScheduleRepository(this.context));
                }
                return (IMasterScheduleRepository)this.repositories[typeOfModel];
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
