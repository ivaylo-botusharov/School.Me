namespace School.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using School.Common;
    using School.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>, IDisposable
    {
        private const int HighestGrade = 12;

        private const int AcademicYearsCount = 3;

        private const int ClassStudentsNumber = 20;

        private const int GradeClassesNumber = 5;

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

        private readonly List<string> generalSchoolThemeSubjectNames = new List<string>()
        {
            "Literature",
            "Languages",
            "Mathematics",
            "Computer Science",
            "Arts",
            "Music",
            "Physical education"
        };

        private readonly List<string> schoolThemeNames = new List<string>()
        {
            "Science, Technology, Engineering, Math (STEM)",
            "Medical Careers (MC)",
            "Humanities (H)",
            "General"
        };

        private readonly DateTime startDate = new DateTime(2012, 9, 15);
        
        private readonly DateTime endDate = new DateTime(2013, 5, 31);

        private int studentCounter = 1;

        private int teacherCounter = 1;

        private UserManager<ApplicationUser> userManager;

        private RoleManager<IdentityRole> roleManager;

        private bool disposed = false;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = this.CreateUserManager(context);
            this.roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            context.Configuration.AutoDetectChangesEnabled = false;

            this.SeedRoles(context);
            this.SeedAdministrators(context);
            this.SeedAcademicYears(context, AcademicYearsCount);

            context.Configuration.AutoDetectChangesEnabled = true;
        }
        
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.roleManager.Dispose();
                }
            }

            this.disposed = true;
        }
        
        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.SuperAdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.TeacherRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.StudentRoleName));

            context.SaveChanges();
        }

        private void SeedAdministrators(ApplicationDbContext context)
        {
            if (context.Administrators.Any())
            {
                return;
            }

            var adminProfile = new Administrator()
            {
                FirstName = "SuperAdmin",
                LastName = "SuperAdmin"
            };

            var adminUser = new ApplicationUser()
            {
                UserName = "superadmin",
                Email = "superadmin@superadmin.com"
            };

            const string Password = "111";

            this.SeedAdminApplicationUser(adminUser, Password);

            adminProfile.ApplicationUser = adminUser;

            context.Administrators.Add(adminProfile);

            adminProfile = new Administrator()
            {
                FirstName = "Admin",
                LastName = "Admin"
            };

            adminUser = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            this.SeedAdminApplicationUser(adminUser, Password);

            adminProfile.ApplicationUser = adminUser;

            context.Administrators.Add(adminProfile);

            context.SaveChanges();
        }

        private void SeedAdminApplicationUser(ApplicationUser adminUser, string password)
        {
            if (!this.roleManager.RoleExists(GlobalConstants.SuperAdministratorRoleName))
            {
                this.roleManager.Create(new IdentityRole(GlobalConstants.SuperAdministratorRoleName));
            }

            if (!this.roleManager.RoleExists(GlobalConstants.AdministratorRoleName))
            {
                this.roleManager.Create(new IdentityRole(GlobalConstants.AdministratorRoleName));
            }

            var result = this.userManager.Create(adminUser, password);

            if (result.Succeeded)
            {
                this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdministratorRoleName);

                if (adminUser.UserName == "superadmin")
                {
                    this.userManager.AddToRole(adminUser.Id, GlobalConstants.SuperAdministratorRoleName);
                }
            }
        }

        private void SeedAcademicYears(ApplicationDbContext context, int academicYearsCount)
        {
            var academicYears = new List<AcademicYear>();

            if (context.AcademicYears.Any())
            {
                return;
            }

            var previousAcademicYear = new AcademicYear()
            {
                StartDate = this.startDate,
                EndDate = this.endDate
            };

            for (int i = 0; i < academicYearsCount; i++)
            {
                if (academicYears.Any())
                {
                    previousAcademicYear = academicYears.Last();
                }

                var academicYear = 
                    this.SeedSingleAcademicYear(context, previousAcademicYear, HighestGrade, GradeClassesNumber);

                academicYears.Add(academicYear);
            }
        }

        private AcademicYear SeedSingleAcademicYear(
            ApplicationDbContext context,
            AcademicYear previousAcademicYear,
            int highestGrade,
            int gradeClassesNumber)
        {
            var academicYear = new AcademicYear();

            if (previousAcademicYear.Grades.Any())
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

            IList<SchoolTheme> previousAcademicYearSchoolThemes = new List<SchoolTheme>();

            if (previousAcademicYear.Grades.Any() && previousAcademicYear.SchoolThemes.Any())
            {
                previousAcademicYearSchoolThemes = previousAcademicYear.SchoolThemes;
            }

            IList<SchoolTheme> schoolThemes = 
                this.SeedSchoolThemes(context, previousAcademicYearSchoolThemes, academicYear);

            academicYear.SchoolThemes = schoolThemes;

            IList<Grade> previousAcademicYearGrades = new List<Grade>();

            if (previousAcademicYear.Grades.Any())
            {
                previousAcademicYearGrades = previousAcademicYear.Grades;
            }

            academicYear.Grades = this.SeedGrades(context, previousAcademicYearGrades, highestGrade, academicYear);
            
            foreach (var grade in academicYear.Grades)
            {
                IList<Subject> previousYearCurrentGradeSubjects = new List<Subject>();

                Grade previousAcademicYearCurrentGrade = previousAcademicYearGrades
                    .FirstOrDefault(g => g.GradeYear == grade.GradeYear);

                if (previousAcademicYearGrades.Any() && previousAcademicYearCurrentGrade != null)
                {
                    previousYearCurrentGradeSubjects = previousAcademicYearCurrentGrade.Subjects;
                }

                this.SeedGradeSubjects(context, schoolThemes, grade, previousYearCurrentGradeSubjects);

                IList<SchoolClass> previousYearCurrentGradeClasses = new List<SchoolClass>();

                if (previousAcademicYearGrades.Any() && previousAcademicYearCurrentGrade != null)
                {
                    previousYearCurrentGradeClasses = previousAcademicYearCurrentGrade.SchoolClasses;
                }

                if (previousAcademicYear.Grades.Any() && grade.GradeYear > 1)
                {
                    gradeClassesNumber = previousAcademicYear
                        .Grades
                        .First(g => g.GradeYear == grade.GradeYear - 1)
                        .SchoolClasses
                        .Count();
                }

                this.SeedGradeSchoolClasses(
                    context, grade, previousYearCurrentGradeClasses, gradeClassesNumber, schoolThemes);
                
                foreach (var schoolClass in grade.SchoolClasses)
                {
                    SchoolClass oldSchoolClass = new SchoolClass();

                    Grade previousAcademicYearPreviousGrade = previousAcademicYear
                        .Grades
                        .FirstOrDefault(g => g.GradeYear == grade.GradeYear - 1);

                    if (previousAcademicYear.Grades.Any() && grade.GradeYear > 1 
                        && previousAcademicYearPreviousGrade != null)
                    {
                        oldSchoolClass = previousAcademicYearPreviousGrade
                            .SchoolClasses
                            .FirstOrDefault(sc => sc.ClassLetter == schoolClass.ClassLetter);
                    }

                    schoolClass.Students = this.SeedSchoolClassStudents(context, oldSchoolClass, ClassStudentsNumber);
                }
            }

            this.SeedTeachers(context, academicYear.Grades, previousAcademicYearGrades);

            context.AcademicYears.AddOrUpdate(academicYear);
            context.SaveChanges();

            return academicYear;
        }

        private IList<SchoolTheme> SeedSchoolThemes(
            ApplicationDbContext context,
            IList<SchoolTheme> previousAcademicYearSchoolThemes,
            AcademicYear currentAcademicYear)
        {
            IList<SchoolTheme> schoolThemes = new List<SchoolTheme>();

            if (previousAcademicYearSchoolThemes.Any())
            {
                foreach (var schoolTheme in previousAcademicYearSchoolThemes)
                {
                    schoolTheme.AcademicYears.Add(currentAcademicYear);
                    context.SchoolThemes.AddOrUpdate(schoolTheme);
                }

                schoolThemes = previousAcademicYearSchoolThemes;
            }
            else
            {
                foreach (var schoolThemeName in this.schoolThemeNames)
                {
                    SchoolTheme schoolTheme = new SchoolTheme()
                    {
                        Name = schoolThemeName
                    };

                    if (schoolThemeName == "General")
                    {
                        schoolTheme.StartGradeYear = 1;
                        schoolTheme.EndGradeYear = 7;
                    }
                    else
                    {
                        schoolTheme.StartGradeYear = 8;
                        schoolTheme.EndGradeYear = 12;
                    }

                    schoolTheme.AcademicYears.Add(currentAcademicYear);

                    context.SchoolThemes.AddOrUpdate(schoolTheme);
                    schoolThemes.Add(schoolTheme);
                }
            }

            return schoolThemes;
        }

        private List<Grade> SeedGrades(
            ApplicationDbContext context,
            IList<Grade> previousAcademicYearGrades,
            int highestGrade,
            AcademicYear currentAcademicYear)
        {
            var grades = new List<Grade>();

            if (previousAcademicYearGrades == null || !previousAcademicYearGrades.Any())
            {
                for (int gradeIndex = 0; gradeIndex < highestGrade; gradeIndex++)
                {
                    Grade grade = new Grade();
                    grade.GradeYear = gradeIndex + 1;
                    grade.AcademicYear = currentAcademicYear;

                    context.Grades.AddOrUpdate(grade);
                    grades.Add(grade);
                }
            }
            else
            {
                var grade = new Grade();

                grade.GradeYear = 1;
                grade.AcademicYear = currentAcademicYear;

                context.Grades.AddOrUpdate(grade);
                grades.Add(grade);

                foreach (var previousAcademicYearGrade in previousAcademicYearGrades)
                {
                    if (previousAcademicYearGrade.GradeYear < highestGrade)
                    {
                        grade = new Grade();
                        grade.GradeYear = previousAcademicYearGrade.GradeYear + 1;
                        grade.AcademicYear = currentAcademicYear;

                        context.Grades.AddOrUpdate(grade);
                        grades.Add(grade);
                    }
                }
            }

            return grades;
        }

        private List<Subject> SeedGradeSubjects(
            ApplicationDbContext context,
            IList<SchoolTheme> schoolThemes,
            Grade grade,
            IList<Subject> previousYearCurrentGradeSubjects)
        {
            List<Subject> subjects = new List<Subject>();

            if (previousYearCurrentGradeSubjects != null && previousYearCurrentGradeSubjects.Any())
            {
                // Copies subject information from previous year current grade to the new subjects
                foreach (var previousYearCurrentGradeSubject in previousYearCurrentGradeSubjects)
                {
                    Subject subject = new Subject();
                    subject.Name = previousYearCurrentGradeSubject.Name;
                    subject.Grade = grade;
                    subject.TotalHours = previousYearCurrentGradeSubject.TotalHours;
                    subject.SchoolTheme = previousYearCurrentGradeSubject.SchoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);
                }
            }
            else
            {
                if (grade.GradeYear < 8)
                {
                    SchoolTheme generalSchoolTheme = schoolThemes.FirstOrDefault(st => st.Name == "General");
                    subjects = this.SeedPrimarySchoolGradeSubjects(context, generalSchoolTheme, grade);
                }
                else
                {
                    IList<SchoolTheme> schoolThemesWithoutGeneral = 
                        schoolThemes.Where(st => st.Name != "General").ToList();
                    
                    subjects = this.SeedSecondarySchoolGradeSubjects(context, schoolThemesWithoutGeneral, grade);
                }
            }

            return subjects;
        }

        private List<Subject> SeedPrimarySchoolGradeSubjects(
            ApplicationDbContext context,
            SchoolTheme generalSchoolTheme,
            Grade grade)
        {
            List<Subject> subjects = new List<Subject>();

            foreach (var subjectName in this.generalSchoolThemeSubjectNames)
            {
                Subject subject = new Subject();
                subject.Name = subjectName;
                subject.Grade = grade;
                subject.TotalHours = 80;
                subject.SchoolTheme = generalSchoolTheme;
                context.Subjects.AddOrUpdate(subject);
                subjects.Add(subject);
            }

            return subjects;
        }

        private List<Subject> SeedSecondarySchoolGradeSubjects(
            ApplicationDbContext context,
            IList<SchoolTheme> schoolThemes,
            Grade grade)
        {
            List<Subject> subjects = new List<Subject>();

            foreach (var schoolTheme in schoolThemes)
            {
                if (schoolTheme.Name == "Science, Technology, Engineering, Math (STEM)")
                {
                    Subject subject = new Subject();
                    subject.Name = "Physics";
                    subject.TotalHours = 110;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);

                    subject = new Subject();
                    subject.Name = "Mathematics";
                    subject.TotalHours = 90;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);

                    subject = new Subject();
                    subject.Name = "Chemistry";
                    subject.TotalHours = 70;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);
                }

                if (schoolTheme.Name == "Medical Careers (MC)")
                {
                    Subject subject = new Subject();
                    subject.Name = "Biology";
                    subject.TotalHours = 120;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);

                    subject = new Subject();
                    subject.Name = "Chemistry";
                    subject.TotalHours = 100;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);

                    subject = new Subject();
                    subject.Name = "Physics";
                    subject.TotalHours = 60;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);
                }

                if (schoolTheme.Name == "Humanities (H)")
                {
                    Subject subject = new Subject();
                    subject.Name = "Literature";
                    subject.TotalHours = 100;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);

                    subject = new Subject();
                    subject.Name = "Languages";
                    subject.TotalHours = 90;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);

                    subject = new Subject();
                    subject.Name = "Philosophy";
                    subject.TotalHours = 70;
                    subject.Grade = grade;
                    subject.SchoolTheme = schoolTheme;

                    context.Subjects.AddOrUpdate(subject);
                    subjects.Add(subject);
                }
            }

            return subjects;
        }
        
        private static List<SchoolClass> CopyClassesFromPreviousYearCurrentGrade(
            ApplicationDbContext context,
            Grade grade,
            IList<SchoolClass> previousYearCurrentGradeClasses)
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();

            foreach (var previousYearCurrentGradeClass in previousYearCurrentGradeClasses)
            {
                SchoolClass schoolClass = new SchoolClass();
                schoolClass.Grade = grade;
                schoolClass.ClassLetter = previousYearCurrentGradeClass.ClassLetter;
                schoolClass.SchoolTheme = previousYearCurrentGradeClass.SchoolTheme;

                context.SchoolClasses.AddOrUpdate(schoolClass);
                schoolClasses.Add(schoolClass);
            }

            return schoolClasses;
        }

        private List<SchoolClass> SeedGradeSchoolClasses(
            ApplicationDbContext context, 
            Grade grade,
            IList<SchoolClass> previousYearCurrentGradeClasses,
            int gradeClassesNumber, 
            IList<SchoolTheme> schoolThemes)
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();

            if (previousYearCurrentGradeClasses != null && previousYearCurrentGradeClasses.Any())
            {
                schoolClasses = 
                    CopyClassesFromPreviousYearCurrentGrade(context, grade, previousYearCurrentGradeClasses);
            }
            else
            {
                schoolClasses = this.CreateGradeNewSchoolClasses(context, grade, gradeClassesNumber, schoolThemes);
            }

            return schoolClasses;
        }

        private List<SchoolClass> CreateGradeNewSchoolClasses(
            ApplicationDbContext context, 
            Grade grade, 
            int gradeClassesNumber, 
            IList<SchoolTheme> schoolThemes)
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            int charANumber = 'A';

            for (int currentChar = charANumber; currentChar < charANumber + gradeClassesNumber; currentChar++)
            {
                SchoolClass schoolClass = new SchoolClass();
                schoolClass.Grade = grade;
                schoolClass.ClassLetter = ((char)currentChar).ToString();

                if (schoolClass.Grade.GradeYear < 8)
                {
                    schoolClass.SchoolTheme = schoolThemes.FirstOrDefault(st => st.Name == "General");
                }
                else
                {
                    if (currentChar < charANumber + 2)
                    {
                        schoolClass.SchoolTheme = schoolThemes.FirstOrDefault(
                            st => st.Name == "Science, Technology, Engineering, Math (STEM)");
                    }

                    if (currentChar == charANumber + 2)
                    {
                        schoolClass.SchoolTheme = schoolThemes.FirstOrDefault(st => st.Name == "Medical Careers (MC)");
                    }

                    if (currentChar > charANumber + 2 && currentChar < charANumber + gradeClassesNumber)
                    {
                        schoolClass.SchoolTheme = schoolThemes.FirstOrDefault(st => st.Name == "Humanities (H)");
                    }
                }

                context.SchoolClasses.AddOrUpdate(schoolClass);
                schoolClasses.Add(schoolClass);
            }

            return schoolClasses;
        }

        private List<Student> SeedSchoolClassStudents(
            ApplicationDbContext context,
            SchoolClass oldSchoolClass,
            int classStudentsNumber)
        {
            var students = new List<Student>();

            if (oldSchoolClass != null && oldSchoolClass.Students.Any())
            {
                students = oldSchoolClass.Students;
            }
            else
            {
                students = this.CreateClassOfStudents(context, classStudentsNumber);
            }

            return students;
        }

        private List<Student> CreateClassOfStudents(
            ApplicationDbContext context,
            int classStudentsNumber)
        {
            var students = new List<Student>();
            for (int i = 0; i < classStudentsNumber; i++)
            {
                var student = this.CreateSingleStudent();
                context.Students.Add(student);
                students.Add(student);
            }

            return students;
        }

        private Student CreateSingleStudent()
        {
            var studentProfile = new Student();
            Random rand = new Random();
            studentProfile.Name = this.personNames[rand.Next(0, this.personNames.Count() - 1)];

            // Create Student Role if it does not exist
            if (!this.roleManager.RoleExists(GlobalConstants.StudentRoleName))
            {
                this.roleManager.Create(new IdentityRole(GlobalConstants.StudentRoleName));
            }

            // Create Student User with password
            var studentUser = new ApplicationUser();

            studentUser.UserName = "student" + this.studentCounter.ToString("D4");
            studentUser.Email = "s" + this.studentCounter.ToString("D4") + "@s.com";
            this.studentCounter++;

            string password = "111";

            var result = this.userManager.Create(studentUser, password);

            // Add Student User to Student Role
            if (result.Succeeded)
            {
                this.userManager.AddToRole(studentUser.Id, GlobalConstants.StudentRoleName);
            }

            // Add Student User to Student Profile
            studentProfile.ApplicationUser = studentUser;

            return studentProfile;
        }

        private List<Teacher> SeedTeachers(
            ApplicationDbContext context, 
            IList<Grade> currentAcademicYearGrades, 
            IList<Grade> previousAcademicYearGrades)
        {
            var teachers = new List<Teacher>();

            foreach (var grade in currentAcademicYearGrades)
            {
                foreach (var subject in grade.Subjects)
                {
                    var previousAcademicYearGradeSubject = new Subject();
                    var teacherProfile = new Teacher();

                    Grade previousAcademicYearCurrentGrade = previousAcademicYearGrades
                        .FirstOrDefault(g => g.GradeYear == grade.GradeYear);

                    if (previousAcademicYearGrades.Any() && previousAcademicYearCurrentGrade != null)
                    {
                        previousAcademicYearGradeSubject = previousAcademicYearCurrentGrade
                            .Subjects
                            .FirstOrDefault(s => s.Name == subject.Name);

                        if (previousAcademicYearGradeSubject != null)
                        {
                            subject.Teachers = previousAcademicYearGradeSubject.Teachers;
                            context.Subjects.AddOrUpdate(subject);
                        }
                        else
                        {
                            teacherProfile = this.CreateSingleTeacher();
                            subject.Teachers.Add(teacherProfile);
                            context.Teachers.Add(teacherProfile);
                            teachers.Add(teacherProfile);
                        }
                    }
                    else
                    {
                        teacherProfile = this.CreateSingleTeacher();
                        subject.Teachers.Add(teacherProfile);
                        context.Teachers.Add(teacherProfile);
                        teachers.Add(teacherProfile);
                    }
                }
            }

            return teachers;
        }

        private Teacher CreateSingleTeacher()
        {
            var teacherProfile = new Teacher();
            var rand = new Random();
            teacherProfile.Name = this.personNames[rand.Next(0, this.personNames.Count())];

            // Create Teacher Role if it does not exist
            if (!this.roleManager.RoleExists(GlobalConstants.TeacherRoleName))
            {
                this.roleManager.Create(new IdentityRole(GlobalConstants.TeacherRoleName));
            }

            // Create Teacher User with password
            var teacherUser = new ApplicationUser();
            string counter = this.teacherCounter.ToString("D3");
            teacherUser.UserName = "teacher" + counter;
            teacherUser.Email = "t" + counter + "@t.com";
            const string Password = "111";

            this.teacherCounter++;

            var result = this.userManager.Create(teacherUser, Password);

            // Add Teacher User to Teacher Role
            if (result.Succeeded)
            {
                this.userManager.AddToRole(teacherUser.Id, GlobalConstants.TeacherRoleName);
            }

            // Add Teacher User to Teacher Profile
            teacherProfile.ApplicationUser = teacherUser;

            return teacherProfile;
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
