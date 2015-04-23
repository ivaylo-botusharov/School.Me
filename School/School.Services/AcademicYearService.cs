namespace School.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;
    
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository academicYearRepository;

        private readonly IGradeRepository gradeRepository;

        private readonly ISchoolClassRepository schoolClassRepository;

        private readonly ISchoolThemeRepository schoolThemeRepository;

        private readonly ISubjectRepository subjectRepository;
        
        public AcademicYearService(
            IAcademicYearRepository academicYearRepository, 
            IGradeRepository gradeRepository,
            ISchoolClassRepository schoolClassRepository,
            ISchoolThemeRepository schoolThemeRepository,
            ISubjectRepository subjectRepository)
        {
            this.academicYearRepository = academicYearRepository;
            this.gradeRepository = gradeRepository;
            this.schoolClassRepository = schoolClassRepository;
            this.schoolThemeRepository = schoolThemeRepository;
            this.subjectRepository = subjectRepository;
        }

        public IQueryable<AcademicYear> All()
        {
            return this.academicYearRepository.All();
        }

        public AcademicYear GetById(Guid id)
        {
            return this.academicYearRepository.GetById(id);
        }

        public void Add(AcademicYear academicYear, int highestGrade)
        {
            AcademicYear previousAcademicYear = this
                .academicYearRepository.All().OrderByDescending(ay => ay.StartDate).FirstOrDefault();

            previousAcademicYear = previousAcademicYear ?? new AcademicYear();

            this.CreateSingleAcademicYear(academicYear, previousAcademicYear, highestGrade);
        }

        public void Add(AcademicYear academicYear)
        {
            AcademicYear previousAcademicYear = this
                .academicYearRepository.All().OrderByDescending(ay => ay.StartDate).FirstOrDefault();

            previousAcademicYear = previousAcademicYear ?? new AcademicYear();

            const int HighestGrade = 12;

            this.CreateSingleAcademicYear(academicYear, previousAcademicYear, HighestGrade);
        }

        public void Update(AcademicYear academicYear)
        {
            this.academicYearRepository.Update(academicYear);
            this.academicYearRepository.SaveChanges();
        }

        public void Delete(AcademicYear academicYear)
        {
            this.academicYearRepository.Delete(academicYear);
            this.academicYearRepository.SaveChanges();
        }

        public void HardDelete(AcademicYear academicYear)
        {
            this.academicYearRepository.HardDelete(academicYear);
            this.academicYearRepository.SaveChanges();

            AcademicYear latestAcademicYear =
                this.academicYearRepository.All().OrderByDescending(ay => ay.StartDate).FirstOrDefault();

            if (latestAcademicYear != null)
            {
                latestAcademicYear.IsActive = true;
                this.academicYearRepository.Update(latestAcademicYear);
                this.academicYearRepository.SaveChanges();
            }
        }

        // Checks if AcademicYear with specified start year or end year exists in database
        public bool AcademicYearExistsInDb(DateTime startDate, DateTime endDate)
        {
            return this.academicYearRepository.ExistsInDb(startDate, endDate);
        }

        public bool AcademicYearUniqueOnEdit(Guid id, DateTime startDate, DateTime endDate)
        {
            return this.academicYearRepository.IsUniqueOnEdit(id, startDate, endDate);
        }

        // Helpers
        private AcademicYear CreateSingleAcademicYear(
            AcademicYear academicYear, AcademicYear previousAcademicYear, int highestGrade)
        {
            academicYear.IsActive = true;
            previousAcademicYear.IsActive = false;
            academicYear.SchoolThemes = this.CreateSchoolThemes(previousAcademicYear.SchoolThemes, academicYear);

            IList<Grade> previousAcademicYearGrades = previousAcademicYear.Grades ?? new List<Grade>();

            academicYear.Grades = this.CreateGrades(previousAcademicYearGrades, academicYear, highestGrade);

            foreach (var grade in academicYear.Grades)
            {
                IList<Subject> previousYearCurrentGradeSubjects = new List<Subject>();

                Grade previousAcademicYearCurrentGrade = previousAcademicYearGrades
                    .FirstOrDefault(g => g.GradeYear == grade.GradeYear);

                if (previousAcademicYearCurrentGrade != null)
                {
                    previousYearCurrentGradeSubjects = previousAcademicYearCurrentGrade.Subjects;
                }
                else
                {
                    previousYearCurrentGradeSubjects = new List<Subject>();
                }

                grade.Subjects = this.CreateGradeSubjects(grade, previousYearCurrentGradeSubjects);

                IList<SchoolClass> previousYearPreviousGradeClasses = new List<SchoolClass>();

                if (previousAcademicYear.Grades != null && previousAcademicYear.Grades.Any() && grade.GradeYear > 1)
                {
                    previousYearPreviousGradeClasses = 
                        previousAcademicYear.Grades.First(g => g.GradeYear == grade.GradeYear - 1).SchoolClasses;
                }

                IList<SchoolClass> previousYearCurrentGradeClasses = new List<SchoolClass>();

                if (previousAcademicYearCurrentGrade != null && previousAcademicYearCurrentGrade.SchoolClasses != null)
                {
                    previousYearCurrentGradeClasses =
                        previousAcademicYearCurrentGrade.SchoolClasses;
                }

                this.CreateGradeSchoolClasses(
                    grade, previousYearPreviousGradeClasses, previousYearCurrentGradeClasses);

                foreach (var schoolClass in grade.SchoolClasses)
                {
                    SchoolClass oldSchoolClass = new SchoolClass();

                    if (previousAcademicYear.Grades != null && 
                        previousAcademicYear.Grades.Any() &&
                        grade.GradeYear > 1)
                    {
                        oldSchoolClass = previousYearPreviousGradeClasses
                            .FirstOrDefault(sc => sc.ClassLetter == schoolClass.ClassLetter);
                    }

                    schoolClass.Students = this.CreateSchoolClassStudents(oldSchoolClass);
                }
            }

            this.CreateTeachers(academicYear.Grades, previousAcademicYearGrades);

            this.academicYearRepository.Add(academicYear);
            this.academicYearRepository.SaveChanges();

            return academicYear;
        }

        private List<Grade> CreateGrades(
            IList<Grade> previousAcademicYearGrades,
            AcademicYear currentAcademicYear,
            int currentYearHighestGrade)
        {
            var grades = new List<Grade>();

            if (previousAcademicYearGrades == null || !previousAcademicYearGrades.Any())
            {
                for (int gradeIndex = 0; gradeIndex < currentYearHighestGrade; gradeIndex++)
                {
                    Grade grade = new Grade();
                    grade.GradeYear = gradeIndex + 1;
                    grade.AcademicYear = currentAcademicYear;
                    grade.AcademicYearId = currentAcademicYear.Id;

                    this.gradeRepository.Add(grade);
                    grades.Add(grade);
                }
            }
            else
            {
                Grade grade = new Grade();
                grade.GradeYear = 1;
                grade.AcademicYear = currentAcademicYear;

                this.gradeRepository.Add(grade);
                grades.Add(grade);

                foreach (var previousAcademicYearGrade in previousAcademicYearGrades)
                {
                    if (previousAcademicYearGrade.GradeYear < currentYearHighestGrade)
                    {
                        grade = new Grade();
                        grade.GradeYear = previousAcademicYearGrade.GradeYear + 1;
                        grade.AcademicYear = currentAcademicYear;

                        this.gradeRepository.Add(grade);
                        grades.Add(grade);
                    }
                }
            }

            return grades;
        }

        private List<SchoolClass> CreateGradeSchoolClasses(
            Grade grade,
            IList<SchoolClass> previousYearPreviousGradeClasses,
            IList<SchoolClass> previousYearCurrentGradeClasses)
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();

            if (previousYearPreviousGradeClasses != null && grade.GradeYear > 1)
            {
                foreach (var previousYearPreviousGradeClass in previousYearPreviousGradeClasses)
                {
                    bool schoolThemesMatching = false;

                    SchoolClass previousYearCurrentGradeClass = previousYearCurrentGradeClasses
                        .FirstOrDefault(c => c.ClassLetter == previousYearPreviousGradeClass.ClassLetter);

                    schoolThemesMatching = this
                        .AreClassesSchoolThemesMatching(previousYearCurrentGradeClass, previousYearPreviousGradeClass);

                    if (schoolThemesMatching)
                    {
                        SchoolClass schoolClass = new SchoolClass();
                        schoolClass.Grade = grade;
                        schoolClass.ClassLetter = previousYearPreviousGradeClass.ClassLetter;

                        schoolClass.SchoolTheme = previousYearPreviousGradeClass.SchoolTheme;
                        schoolClass.SchoolThemeId = previousYearPreviousGradeClass.SchoolTheme.Id;

                        this.schoolClassRepository.Add(schoolClass);
                        schoolClasses.Add(schoolClass);
                    }
                }
            }
           
            return schoolClasses;
        }

        private bool AreClassesSchoolThemesMatching(
            SchoolClass firstSchoolClass, 
            SchoolClass secondSchoolClass)
        {
            bool areSchoolThemesMatching = false;

            if (firstSchoolClass != null && 
                secondSchoolClass != null && 
                firstSchoolClass.SchoolTheme != null && 
                secondSchoolClass.SchoolTheme != null)
            {
                if (firstSchoolClass.SchoolTheme.Name == secondSchoolClass.SchoolTheme.Name)
                {
                    areSchoolThemesMatching = true;
                }
            }

            return areSchoolThemesMatching;
        }

        private List<Student> CreateSchoolClassStudents(SchoolClass oldSchoolClass)
        {
            var students = new List<Student>();

            if (oldSchoolClass != null && oldSchoolClass.Students.Any())
            {
                students = oldSchoolClass.Students;
            }

            return students;
        }

        private IList<SchoolTheme> CreateSchoolThemes(
            IList<SchoolTheme> previousAcademicYearSchoolThemes,
            AcademicYear currentAcademicYear)
        {
            IList<SchoolTheme> schoolThemes = new List<SchoolTheme>();

            if (previousAcademicYearSchoolThemes != null && previousAcademicYearSchoolThemes.Any())
            {
                foreach (var schoolTheme in previousAcademicYearSchoolThemes)
                {
                    schoolTheme.AcademicYears.Add(currentAcademicYear);
                    this.schoolThemeRepository.Update(schoolTheme);
                }

                schoolThemes = previousAcademicYearSchoolThemes;
            }

            return schoolThemes;
        }

        private List<Subject> CreateGradeSubjects(Grade grade, IList<Subject> previousAcademicYearCurrentGradeSubjects)
        {
            List<Subject> subjects = new List<Subject>();

            foreach (var previousYearSubject in previousAcademicYearCurrentGradeSubjects)
            {
                Subject subject = new Subject();
                subject.Name = previousYearSubject.Name;
                subject.Grade = grade;
                subject.TotalHours = previousYearSubject.TotalHours;
                subject.SchoolTheme = previousYearSubject.SchoolTheme;
                subject.SchoolThemeId = previousYearSubject.SchoolThemeId;
                this.subjectRepository.Add(subject);
                subjects.Add(subject);
            }

            return subjects;
        }

        private void CreateTeachers(
            IList<Grade> currentAcademicYearGrades, IList<Grade> previousAcademicYearGrades)
        {
            foreach (var grade in currentAcademicYearGrades)
            {
                foreach (var subject in grade.Subjects)
                {
                    Subject previousAcademicYearGradeSubject = new Subject();

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
                        }
                    }
                }
            }
        }
    }
}
