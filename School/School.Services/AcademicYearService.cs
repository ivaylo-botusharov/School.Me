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

        public void Add(AcademicYear academicYear)
        {
            int lastSchoolYear = 12;

            AcademicYear previousAcademicYear = this.academicYearRepository.All().OrderByDescending(ay => ay.StartDate).FirstOrDefault();

            if (previousAcademicYear == null)
            {
                previousAcademicYear = new AcademicYear();
            }

            this.CreateSingleAcademicYear(academicYear, previousAcademicYear, lastSchoolYear);
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

        // Checks if AcademicYear with specified start year or end year exists in database
        public bool AcademicYearExistsInDb(DateTime startDate, DateTime endDate)
        {
            return this.academicYearRepository.ExistsInDb(startDate, endDate);
        }

        private AcademicYear CreateSingleAcademicYear(AcademicYear academicYear, AcademicYear previousAcademicYear, int lastSchoolYear)
        {
            academicYear.IsActive = true;
            previousAcademicYear.IsActive = false;

            academicYear.SchoolThemes = this.CreateSchoolThemes(previousAcademicYear.SchoolThemes, academicYear);

            IList<Grade> previousAcademicYearGrades = new List<Grade>();

            if (previousAcademicYear.Grades.Count() > 0)
            {
                previousAcademicYearGrades = previousAcademicYear.Grades;
            }

            academicYear.Grades = this.CreateGrades(previousAcademicYearGrades, lastSchoolYear, academicYear);

            foreach (var grade in academicYear.Grades)
            {
                IList<Subject> previousYearCurrentGradeSubjects = new List<Subject>();

                if (previousAcademicYear.Grades != null && previousAcademicYear.Grades.Count() > 0)
                {
                    previousYearCurrentGradeSubjects = previousAcademicYearGrades
                        .FirstOrDefault(g => g.GradeYear == grade.GradeYear).Subjects;
                }

                grade.Subjects = this.CreateGradeSubjects(grade, previousYearCurrentGradeSubjects);

                IList<SchoolClass> previousYearPreviousGradeClasses = new List<SchoolClass>();

                if (previousAcademicYear.Grades.Count() > 0 && grade.GradeYear > 1)
                {
                    previousYearPreviousGradeClasses = previousAcademicYear
                        .Grades
                        .First(g => g.GradeYear == grade.GradeYear - 1)
                        .SchoolClasses;
                }

                IList<SchoolClass> previousYearCurrentGradeClasses = new List<SchoolClass>();

                if (previousAcademicYearGrades != null && previousAcademicYearGrades.Count() > 0)
                {
                    previousYearCurrentGradeClasses = previousAcademicYearGrades
                        .FirstOrDefault(g => g.GradeYear == grade.GradeYear)
                        .SchoolClasses;
                }

                this.CreateGradeSchoolClasses(grade, previousYearPreviousGradeClasses, previousYearCurrentGradeClasses);

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
            int lastSchoolYear,
            AcademicYear currentAcademicYear)
        {
            var grades = new List<Grade>();

            if (previousAcademicYearGrades == null || previousAcademicYearGrades.Count() == 0)
            {
                for (int gradeIndex = 0; gradeIndex < lastSchoolYear; gradeIndex++)
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
                    if (previousAcademicYearGrade.GradeYear < lastSchoolYear)
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

                    SchoolClass previousYearCurrentGradeClass =
                        previousYearCurrentGradeClasses.FirstOrDefault(c => c.ClassLetter == previousYearPreviousGradeClass.ClassLetter);

                    schoolThemesMatching = this.AreClassesSchoolThemesMatching(previousYearCurrentGradeClass, previousYearPreviousGradeClass);

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

            if (oldSchoolClass != null && oldSchoolClass.Students.Count() > 0)
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

            if (previousAcademicYearSchoolThemes != null && previousAcademicYearSchoolThemes.Count() > 0)
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

        private List<Teacher> CreateTeachers(IList<Grade> currentAcademicYearGrades, IList<Grade> previousAcademicYearGrades)
        {
            var teachers = new List<Teacher>();

            foreach (var grade in currentAcademicYearGrades)
            {
                foreach (var subject in grade.Subjects)
                {
                    Subject previousAcademicYearGradeSubject = new Subject();

                    if (previousAcademicYearGrades.Count() > 0)
                    {
                        previousAcademicYearGradeSubject = previousAcademicYearGrades
                            .FirstOrDefault(g => g.GradeYear == grade.GradeYear)
                            .Subjects
                            .FirstOrDefault(s => s.Name == subject.Name);

                        if (previousAcademicYearGradeSubject != null)
                        {
                            subject.Teachers = previousAcademicYearGradeSubject.Teachers;
                        }
                    }
                }
            }

            return teachers;
        }
    }
}
