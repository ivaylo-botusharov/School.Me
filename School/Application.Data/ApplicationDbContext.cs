namespace Application.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using Application.Data.Migrations;
    using Application.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Teacher> Teachers { get; set; }

        public IDbSet<Administrator> Administrators { get; set; }

        public IDbSet<Subject> Subjects { get; set; }

        public IDbSet<SchoolClass> SchoolClasses { get; set; }

        public IDbSet<Lesson> Lessons { get; set; }

        public IDbSet<Attachment> Attachments { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<MasterSchedule> MasterSchedules { get; set; }

        public IDbSet<AcademicYear> AcademicYears { get; set; }

        public IDbSet<HomeworkSolution> HomeworkSolutions { get; set; }

        public IDbSet<TotalScore> TotalScores { get; set; }

        public IDbSet<Grade> Grades { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public new void SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
