namespace School.Services
{
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class AcademicYearService : IAcademicYearService
    {
        private readonly UnitOfWork unitOfWork;
        public AcademicYearService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public IQueryable<AcademicYear> All()
        {
            return this.unitOfWork.AcademicYears.All();
        }

        public AcademicYear GetById(Guid id)
        {
            return this.unitOfWork.AcademicYears.GetById(id);
        }

        public void Add(AcademicYear academicYear)
        {
            int lastSchoolYear = 12;
            int gradeClassesNumber = 5;

            AcademicYear previousAcademicYear = this.unitOfWork.AcademicYears.All().OrderByDescending(ay => ay.StartDate).FirstOrDefault();

            if (previousAcademicYear == null)
            {
                previousAcademicYear = new AcademicYear();
            }

            SeedSingleAcademicYear(academicYear, previousAcademicYear, lastSchoolYear, gradeClassesNumber);
        }

        public void Update(AcademicYear academicYear)
        {
            this.unitOfWork.AcademicYears.Update(academicYear);
            this.unitOfWork.Save();
        }

        public void Delete(AcademicYear academicYear)
        {
            this.unitOfWork.AcademicYears.Delete(academicYear);
            this.unitOfWork.Save();
        }

        //Checks if AcademicYear with specified start year or end year exists in database
        public bool AcademicYearExistsInDb(DateTime startDate, DateTime endDate)
        {
            return this.unitOfWork.AcademicYears.ExistsInDb(startDate, endDate);
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

        private AcademicYear SeedSingleAcademicYear(
            AcademicYear academicYear,
            AcademicYear previousAcademicYear,
            int lastSchoolYear,
            int gradeClassesNumber)
        {
            academicYear.IsActive = true;
            previousAcademicYear.IsActive = false;

            academicYear.SchoolThemes = SeedSchoolThemes(previousAcademicYear.SchoolThemes, academicYear);

            IList<Grade> previousAcademicYearGrades = new List<Grade>();
            int previousAcademicYearNumber = 0;

            if (previousAcademicYear.Grades.Count() > 0)
            {
                previousAcademicYearGrades = previousAcademicYear.Grades;
                previousAcademicYearNumber = previousAcademicYear.StartDate.Year;
            }

            academicYear.Grades = SeedGrades(previousAcademicYearGrades, lastSchoolYear, academicYear);

            foreach (var grade in academicYear.Grades)
            {
                IList<Subject> previousYearCurrentGradeSubjects = new List<Subject>();

                if (previousAcademicYear.Grades != null && previousAcademicYear.Grades.Count() > 0)
                {
                    previousYearCurrentGradeSubjects = previousAcademicYearGrades
                        .FirstOrDefault(g => g.GradeYear == grade.GradeYear).Subjects;
                }

                grade.Subjects = SeedGradeSubjects(grade, previousYearCurrentGradeSubjects);

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

                SeedGradeSchoolClasses(grade, previousYearPreviousGradeClasses, previousYearCurrentGradeClasses);

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

                    schoolClass.Students = SeedSchoolClassStudents(oldSchoolClass);
                }
            }

            SeedTeachers(academicYear.Grades, previousAcademicYearGrades);

            this.unitOfWork.AcademicYears.Add(academicYear);
            this.unitOfWork.Save();

            return academicYear;
        }

        private List<Grade> SeedGrades(
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

                    this.unitOfWork.Grades.Add(grade);
                    grades.Add(grade);
                }
            }
            else
            {
                Grade grade = new Grade();
                grade.GradeYear = 1;
                grade.AcademicYear = currentAcademicYear;

                this.unitOfWork.Grades.Add(grade);
                grades.Add(grade);

                foreach (var previousAcademicYearGrade in previousAcademicYearGrades)
                {
                    if (previousAcademicYearGrade.GradeYear < lastSchoolYear)
                    {
                        grade = new Grade();
                        grade.GradeYear = previousAcademicYearGrade.GradeYear + 1;
                        grade.AcademicYear = currentAcademicYear;

                        this.unitOfWork.Grades.Add(grade);
                        grades.Add(grade);
                    }
                }
            }

            return grades;
        }

        private List<SchoolClass> SeedGradeSchoolClasses(
            Grade grade,
            IList<SchoolClass> previousYearPreviousGradeClasses,
            IList<SchoolClass> previousYearCurrentGradeClasses
            )
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();

            if (previousYearPreviousGradeClasses != null && grade.GradeYear > 1)
            {
                foreach (var previousYearPreviousGradeClass in previousYearPreviousGradeClasses)
                {
                    bool schoolThemesMatching = false;

                    SchoolClass previousYearCurrentGradeClass =
                        previousYearCurrentGradeClasses.FirstOrDefault(c => c.ClassLetter == previousYearPreviousGradeClass.ClassLetter);

                    schoolThemesMatching = AreClassesSchoolThemesMatching(previousYearCurrentGradeClass, previousYearPreviousGradeClass);

                    if (schoolThemesMatching)
                    {
                        SchoolClass schoolClass = new SchoolClass();
                        schoolClass.Grade = grade;
                        schoolClass.ClassLetter = previousYearPreviousGradeClass.ClassLetter;

                        schoolClass.SchoolTheme = previousYearPreviousGradeClass.SchoolTheme;
                        schoolClass.SchoolThemeId = previousYearPreviousGradeClass.SchoolTheme.Id;

                        this.unitOfWork.SchoolClasses.Add(schoolClass);
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

        private List<Student> SeedSchoolClassStudents(SchoolClass oldSchoolClass)
        {
            var students = new List<Student>();

            if (oldSchoolClass != null && oldSchoolClass.Students.Count() > 0)
            {
                students = oldSchoolClass.Students;
            }

            return students;
        }

        private IList<SchoolTheme> SeedSchoolThemes(
            IList<SchoolTheme> previousAcademicYearSchoolThemes,
            AcademicYear currentAcademicYear)
        {
            IList<SchoolTheme> schoolThemes = new List<SchoolTheme>();

            if (previousAcademicYearSchoolThemes != null && previousAcademicYearSchoolThemes.Count() > 0)
            {
                foreach (var schoolTheme in previousAcademicYearSchoolThemes)
                {
                    schoolTheme.AcademicYears.Add(currentAcademicYear);
                    this.unitOfWork.SchoolThemes.Update(schoolTheme);
                }

                schoolThemes = previousAcademicYearSchoolThemes;
            }

            return schoolThemes;
        }

        private List<Subject> SeedGradeSubjects(Grade grade, IList<Subject> previousAcademicYearCurrentGradeSubjects)
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
                this.unitOfWork.Subjects.Add(subject);
                subjects.Add(subject);
            }

            return subjects;
        }

        private List<Teacher> SeedTeachers(IList<Grade> currentAcademicYearGrades, IList<Grade> previousAcademicYearGrades)
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
