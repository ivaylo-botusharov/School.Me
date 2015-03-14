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

        private const int academicYearsCount = 3;

        private DateTime startDate = new DateTime(1995, 9, 15);
        private DateTime endDate = new DateTime(1996, 5, 31);

        private const int classStudentsNumber = 20;
        private const int gradeClassesNumber = 5;

        private int studentCounter = 1;

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

        private UserManager<ApplicationUser> userManager;

        private RoleManager<IdentityRole> roleManager;

        //private ApplicationDbContext context;

        protected override void Seed(Application.Data.ApplicationDbContext context)
        {
            userManager = this.CreateUserManager(context);
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            context.Configuration.AutoDetectChangesEnabled = false;

            this.SeedRoles(context);
            this.SeedAdminUser(context);
            this.SeedAcademicYears(context, academicYearsCount);

            //var teachers = this.SeedTeachers(context);

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

        private void SeedAdminUser(ApplicationDbContext context)
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

        private void SeedAcademicYears(ApplicationDbContext context, int academicYearsCount)
        {
            var academicYears = new List<AcademicYear>();

            if (context.AcademicYears.Any())
            {
                return;
            }

            var previousAcademicYear = new AcademicYear();

            previousAcademicYear.StartDate = startDate;
            previousAcademicYear.EndDate = endDate;

            for (int i = 0; i < academicYearsCount; i++)
            {
                if (academicYears.Count() > 0)
                {
                    previousAcademicYear = academicYears.Last();
                }

                var academicYear = SeedSingleAcademicYear(context, previousAcademicYear, lastSchoolYear, gradeClassesNumber);
                academicYears.Add(academicYear);
            }
        }

        private AcademicYear SeedSingleAcademicYear(
            ApplicationDbContext context,
            AcademicYear previousAcademicYear,
            int lastSchoolYear,
            int gradeClassesNumber)
        {
            var academicYear = new AcademicYear();

            if (previousAcademicYear.Grades.Count() > 0)
            {
                academicYear.StartDate = previousAcademicYear.StartDate.AddYears(1);
                academicYear.EndDate = previousAcademicYear.EndDate.AddYears(1);
            }
            else
            {
                academicYear.StartDate = previousAcademicYear.StartDate;
                academicYear.EndDate = previousAcademicYear.EndDate;
            }

            academicYear.IsActive = true;
            previousAcademicYear.IsActive = false;

            IList<Grade> previousAcademicYearGrades = new List<Grade>();

            if (previousAcademicYear.Grades.Count() > 0)
            {
                previousAcademicYearGrades = previousAcademicYear.Grades;
            }

            academicYear.Grades = SeedGrades(context, previousAcademicYearGrades, lastSchoolYear);

            foreach (var grade in academicYear.Grades)
            {
                if (previousAcademicYear.Grades.Count() > 0 && grade.GradeYear > 1)
                {
                    gradeClassesNumber = previousAcademicYear
                        .Grades
                        .First(g => g.GradeYear == grade.GradeYear - 1)
                        .SchoolClasses
                        .Count();
                }

                SeedSchoolClasses(context, grade, gradeClassesNumber);

                foreach (var schoolClass in grade.SchoolClasses)
                {
                    SchoolClass oldSchoolClass = new SchoolClass();

                    if (previousAcademicYear.Grades.Count() > 0 && grade.GradeYear > 1)
                    {
                        oldSchoolClass = previousAcademicYear
                           .Grades
                           .FirstOrDefault(g => g.GradeYear == grade.GradeYear - 1)
                           .SchoolClasses
                           .FirstOrDefault(sc => sc.ClassLetter == schoolClass.ClassLetter);
                    }

                    schoolClass.Students = SeedStudents(context, oldSchoolClass, classStudentsNumber);
                }
            }

            context.AcademicYears.AddOrUpdate(academicYear);
            context.SaveChanges();

            return academicYear;
        }

        private List<Grade> SeedGrades(
            ApplicationDbContext context,
            IList<Grade> previousAcademicYearGrades,
            int lastSchoolYear)
        {
            var grades = new List<Grade>();

            if (previousAcademicYearGrades == null || previousAcademicYearGrades.Count() == 0)
            {
                for (int gradeIndex = 0; gradeIndex < lastSchoolYear; gradeIndex++)
                {
                    Grade grade = new Grade();
                    grade.GradeYear = gradeIndex + 1;

                    context.Grades.AddOrUpdate(grade);
                    grades.Add(grade);
                }
            }
            else
            {
                Grade grade = new Grade();
                grade.GradeYear = 1;

                context.Grades.AddOrUpdate(grade);
                grades.Add(grade);

                foreach (var previousAcademicYearGrade in previousAcademicYearGrades)
                {
                    if (previousAcademicYearGrade.GradeYear < lastSchoolYear)
                    {
                        grade = new Grade();
                        grade.GradeYear = previousAcademicYearGrade.GradeYear + 1;

                        context.Grades.AddOrUpdate(grade);
                        grades.Add(grade);
                    }
                }
            }

            return grades;
        }

        private List<SchoolClass> SeedSchoolClasses(ApplicationDbContext context, Grade grade, int gradeClassesNumber)
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            int charANumber = (int)'A';

            for (int currentChar = charANumber; currentChar < charANumber + gradeClassesNumber; currentChar++)
            {
                SchoolClass schoolClass = new SchoolClass();
                schoolClass.Grade = grade;
                schoolClass.ClassLetter = ((char)currentChar).ToString();

                context.SchoolClasses.AddOrUpdate(schoolClass);
                schoolClasses.Add(schoolClass);
            }

            return schoolClasses;
        }

        private List<Student> SeedStudents(
            ApplicationDbContext context,
            SchoolClass oldSchoolClass,
            int classStudentsNumber)
        {
            var students = new List<Student>();

            if (oldSchoolClass != null && oldSchoolClass.Students.Count() > 0)
            {
                students = oldSchoolClass.Students;
            }
            else
            {
                students = CreateClassOfStudents(context, classStudentsNumber);
            }

            return students;
        }

        private Student CreateSingleStudent()
        {
            var studentProfile = new Student();
            Random rand = new Random();
            studentProfile.Name = this.personNames[rand.Next(0, this.personNames.Count() - 1)];

            // Create Student Role if it does not exist
            if (!roleManager.RoleExists(GlobalConstants.StudentRoleName))
            {
                roleManager.Create(new IdentityRole(GlobalConstants.StudentRoleName));
            }

            // Create Student User with password
            var studentUser = new ApplicationUser();

            studentUser.UserName = "student" + studentCounter.ToString("D4");
            studentUser.Email = "s" + studentCounter.ToString("D4") + "@s.com";
            studentCounter++;

            string password = "111";

            var result = userManager.Create(studentUser, password);

            // Add Student User to Student Role
            if (result.Succeeded)
            {
                userManager.AddToRole(studentUser.Id, GlobalConstants.StudentRoleName);
            }

            // Add Student User to Student Profile
            studentProfile.ApplicationUser = studentUser;

            return studentProfile;

        }

        private List<Student> CreateClassOfStudents(
            ApplicationDbContext context,
            int classStudentsNumber)
        {
            var students = new List<Student>();
            for (int i = 0; i < classStudentsNumber; i++)
            {
                var student = CreateSingleStudent();
                context.Students.Add(student);
                students.Add(student);
            }
            return students;
        }

        private List<Teacher> SeedTeachers(ApplicationDbContext context)
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
