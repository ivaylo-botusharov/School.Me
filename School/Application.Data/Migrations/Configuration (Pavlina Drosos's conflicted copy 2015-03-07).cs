namespace Application.Data.Migrations
{
    using Application.Common;
    using Application.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Application.Data.ApplicationDbContext>
    {
        private const int lastSchoolYear = 12;
        
        private const int academicYearStartDate = 15;
        private const int academicYearStartMonth = 9;

        private const int academicYearEndDate = 31;
        private const int academicYearEndMonth = 5;
        
        private const int firstAcademicYear = 1995;
        private const int academicYearsCount = 3;

        private readonly List<string> personNames = new List<string>()
        {
            "Teddy Ferrara",
            "Dyan Fisher",
            "Anne Smith",
            "Maria Finnegan",
            "Ronnie Foltz",
            "Eleanor Fowler",
            "William Heller",
            "Bobbi Canfield",
            "Christina Buxton",
            "Alexander Byrnes",
            "Simon Cambell",
            "Peter Callaghan",
            "Ashley Hong",
            "Hayden Jacques",
            "Ida Jacobson",
            "Jamie Miller",
            "Jason Peterson",
            "Michael Kaiser",
            "Ivy Kearney",
            "Sammy Keen",
        };

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Application.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var userManager = this.CreateUserManager(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            context.Configuration.AutoDetectChangesEnabled = false;

            this.SeedRoles(context);
            this.SeedAdminUser(context, userManager, roleManager);
            var academicYears = this.SeedAcademicYears(context);
            var schoolClasses = this.SeedSchoolClasses(context, academicYears);
            var teachers = this.SeedTeachers(context, userManager, roleManager);
            var students = this.SeedStudents(context, userManager, roleManager);

            context.Configuration.AutoDetectChangesEnabled = true;
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.TeacherRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.StudentRoleName));

            context.SaveChanges();
        }

        private void SeedAdminUser(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (context.Administrators.Any())
            {
                return;
            }

            var administratorProfile = new Administrator();
            administratorProfile.FirstName = "Admin";
            administratorProfile.LastName = "Admin";
            

            // Create Admin Role if it does not exist
            if (!roleManager.RoleExists(GlobalConstants.AdministratorRoleName))
            {
                roleManager.Create(new IdentityRole(GlobalConstants.AdministratorRoleName));
            }

            // Create Admin User with password
            var administratorUser = new ApplicationUser();
            administratorUser.UserName = "admin";
            administratorUser.Email = "admin@admin.com";

            string password = "111";

            var result = userManager.Create(administratorUser, password);

            // Add Admin User to Admin Role
            if (result.Succeeded)
            {
                userManager.AddToRole(administratorUser.Id, GlobalConstants.AdministratorRoleName);
            }

            // Add Admin User to Admin Profile
            administratorProfile.ApplicationUser = administratorUser;
            context.Administrators.Add(administratorProfile);

            context.SaveChanges();
        }

        private List<AcademicYear> SeedAcademicYears(ApplicationDbContext context)
        {
            var academicYears = new List<AcademicYear>();

            if (context.AcademicYears.Any())
            {
                return academicYears;
            }

            for (int i = 0; i < academicYearsCount; i++)
            {
                var academicYear = new AcademicYear();
                academicYear.StartDate = new DateTime(firstAcademicYear + i, academicYearStartMonth, academicYearStartDate);
                academicYear.EndDate = new DateTime(firstAcademicYear + i + 1, academicYearEndMonth, academicYearEndDate);
                academicYear.IsActive = false;

                if (i == academicYearsCount - 1)
                {
                    academicYear.IsActive = true;
                }

                context.AcademicYears.AddOrUpdate(academicYear);
                academicYears.Add(academicYear);
            }

            context.SaveChanges();

            return academicYears;
        }

        private List<SchoolClass> SeedSchoolClasses(ApplicationDbContext context, List<AcademicYear> academicYears)
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            int academicYearsCount = academicYears.Count();
            

            if (context.SchoolClasses.Any())
            {
                return schoolClasses;
            }

            int maxGrades; 

            for (int currentYear = 1; currentYear <= academicYearsCount; currentYear++)
            {
                maxGrades = currentYear > lastSchoolYear ? lastSchoolYear : currentYear;

                for (int currentGrade = 1; currentGrade <= maxGrades; currentGrade++)
                {

                    for (char letter = 'A'; letter <= 'E'; letter++)
                    {
                        SchoolClass schoolClass = new SchoolClass();
                        schoolClass.Grade = currentGrade;
                        schoolClass.ClassLetter = letter.ToString();
                        schoolClass.AcademicYear = academicYears[currentYear - 1];
                        context.SchoolClasses.AddOrUpdate(schoolClass);
                        schoolClasses.Add(schoolClass);
                    }
                }
            }

            context.SaveChanges();

            return schoolClasses;
        }

        private List<Teacher> SeedTeachers(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var teachers = new List<Teacher>();

            if (context.Teachers.Any())
            {
                return teachers;
            }

            for (int i = 1; i <= 20; i++)
            {
                var teacherProfile = new Teacher();
                teacherProfile.Name = this.personNames[20 - i];

                // Create Teacher Role if it does not exist
                if (!roleManager.RoleExists(GlobalConstants.TeacherRoleName))
                {
                    roleManager.Create(new IdentityRole(GlobalConstants.TeacherRoleName));
                }

                // Create Teacher User with password
                var teacherUser = new ApplicationUser();
                string counter = i < 10 ? ("0" + i) : i.ToString();
                teacherUser.UserName = "teacher" + counter;
                teacherUser.Email = "t" + counter + "@t.com";
                string password = "111";

                var result = userManager.Create(teacherUser, password);

                // Add Teacher User to Teacher Role
                if (result.Succeeded)
                {
                    userManager.AddToRole(teacherUser.Id, GlobalConstants.TeacherRoleName);
                }

                // Add Teacher User to Teacher Profile
                teacherProfile.ApplicationUser = teacherUser;
                context.Teachers.Add(teacherProfile);

                teachers.Add(teacherProfile);
            }

            context.SaveChanges();

            return teachers;
        }

        private List<Student> SeedStudents(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var students = new List<Student>();

            if (context.Students.Any())
            {
                return students;
            }

            for (int i = 1; i <= 20; i++)
            {
                var studentProfile = new Student();
                studentProfile.Name = this.personNames[i - 1];

                // Create Student Role if it does not exist
                if (!roleManager.RoleExists(GlobalConstants.StudentRoleName))
                {
                    roleManager.Create(new IdentityRole(GlobalConstants.StudentRoleName));
                }

                // Create Student User with password
                var studentUser = new ApplicationUser();
                string counter = i < 10 ? ("0" + i) : i.ToString();
                studentUser.UserName = "student" + counter;
                studentUser.Email = "s" + counter + "@s.com";
                string password = "111";

                var result = userManager.Create(studentUser, password);

                // Add Student User to Student Role
                if (result.Succeeded)
                {
                    userManager.AddToRole(studentUser.Id, GlobalConstants.StudentRoleName);
                }

                // Add Student User to Student Profile
                studentProfile.ApplicationUser = studentUser;
                context.Students.Add(studentProfile);

                students.Add(studentProfile);
            }

            context.SaveChanges();

            return students;
        }

        private UserManager<ApplicationUser> CreateUserManager(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Configure user manager
            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            return userManager;
        }
    }
}
