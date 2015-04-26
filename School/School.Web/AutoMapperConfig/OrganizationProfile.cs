using School.Web.Areas.Administration.Models;

namespace School.Web.AutoMapperConfig
{
    using System.Linq;
    using AutoMapper;
    using School.Models;
    
    public class OrganizationProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ApplicationUser, School.Web.Areas.Administration.Models.AccountDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AccountDetailsEditModel, ApplicationUser>();
            
            Mapper.CreateMap<School.Web.Areas.Administration.Models.AdministratorRegisterSubmitModel, Administrator>();

            Mapper.CreateMap<Administrator, School.Web.Areas.Administration.Models.AdministratorDetailsEditModel>()
                .ForMember(dest => dest.AccountDetailsEditModel, opt => opt.MapFrom(src => src.ApplicationUser));

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AdministratorDetailsEditModel, Administrator>()
                .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => src.AccountDetailsEditModel));

            Mapper.CreateMap<Administrator, School.Web.Areas.Administration.Models.AdministratorListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AdministratorListViewModel, Administrator>();

            Mapper.CreateMap<Administrator, School.Web.Areas.Administration.Models.AdministratorDeleteSubmitModel>();

            Mapper.CreateMap<School.Web.Areas.Students.Models.StudentRegisterSubmitModel, Student>();

            Mapper.CreateMap<Student, School.Web.Areas.Administration.Models.StudentListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<School.Web.Areas.Administration.Models.StudentListViewModel, Student>();

            Mapper.CreateMap<Student, School.Web.Areas.Administration.Models.StudentDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.StudentDetailsEditModel, Student>();

            Mapper.CreateMap<School.Web.Areas.Teachers.Models.TeacherRegisterSubmitModel, Teacher>();

            Mapper.CreateMap<Teacher, School.Web.Areas.Administration.Models.TeacherListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<Teacher, School.Web.Areas.Administration.Models.TeacherDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.TeacherDetailsEditModel, Teacher>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AcademicYearCreateSubmitModel, AcademicYear>();

            Mapper.CreateMap<AcademicYear, School.Web.Areas.Administration.Models.AcademicYearListViewModel>();

            Mapper.CreateMap<AcademicYear, School.Web.Areas.Administration.Models.AcademicYearDetailsViewModel>()
                .ForMember(dest => dest.Grades, opt => opt.MapFrom(src => src.Grades.OrderBy(g => g.GradeYear)));

            Mapper.CreateMap<AcademicYear, School.Web.Areas.Administration.Models.AcademicYearDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AcademicYearDetailsEditModel, AcademicYear>();

            Mapper.CreateMap<AcademicYear, School.Web.Areas.Administration.Models.AcademicYearDetailsDeleteModel>()
                .ForMember(dest => dest.GradesCount, opt => opt.MapFrom(src => src.Grades.Count()));

            Mapper.CreateMap<Grade, School.Web.Areas.Administration.Models.GradeListViewModel>()
                .ForMember(dest => dest.SchoolClassesCount, opt => opt.MapFrom(src => src.SchoolClasses.Count))
                .ForMember(
                    dest => dest.SchoolClasses, 
                    opt => opt.MapFrom(src => src.SchoolClasses.OrderBy(sc => sc.ClassLetter)));

            Mapper.CreateMap<School.Web.Areas.Administration.Models.GradeCreateSubmitModel, Grade>();

            Mapper.CreateMap<Grade, School.Web.Areas.Administration.Models.GradeCreateSubmitModel>()
                .ForMember(dest => dest.AcademicYearStartDate, opt => opt.MapFrom(src => src.AcademicYear.StartDate))
                .ForMember(dest => dest.AcademicYearEndDate, opt => opt.MapFrom(src => src.AcademicYear.EndDate));

            Mapper.CreateMap<Grade, School.Web.Areas.Administration.Models.GradeDetailsViewModel>()
                .ForMember(dest => dest.AcademicYearStartDate, opt => opt.MapFrom(src => src.AcademicYear.StartDate))
                .ForMember(dest => dest.AcademicYearEndDate, opt => opt.MapFrom(src => src.AcademicYear.EndDate))
                .ForMember(dest => dest.SchoolClassesCount, opt => opt.MapFrom(src => src.SchoolClasses.Count()))
                .ForMember(
                    dest => dest.SchoolClasses, 
                    opt => opt.MapFrom(src => src.SchoolClasses.OrderBy(sc => sc.ClassLetter)));

            Mapper.CreateMap<School.Web.Areas.Administration.Models.GradeDetailsViewModel, Grade>();

            Mapper.CreateMap<SchoolClass, School.Web.Areas.Administration.Models.SchoolClassListViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear))
                .ForMember(
                    dest => dest.StudentsNumber,
                    opt => opt.MapFrom(src => src.Students.Count(s => s.IsDeleted == false)))
                .ForMember(
                    dest => dest.AcademicYearStartDate, 
                    opt => opt.MapFrom(src => src.Grade.AcademicYear.StartDate));

            Mapper.CreateMap<SchoolClass, School.Web.Areas.Administration.Models.SchoolClassDetailsViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear))
                .ForMember(
                    dest => dest.StudentsNumber,
                    opt => opt.MapFrom(src => src.Students.Count(s => s.IsDeleted == false)))
                .ForMember(
                    dest => dest.Students, 
                    opt => opt.MapFrom(src => src.Students.Where(s => s.IsDeleted == false)))
                .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src => src.Grade.AcademicYear));

            Mapper.CreateMap<SchoolClass, SchoolClassEditViewModel>();

            Mapper.CreateMap<SchoolClassEditViewModel, SchoolClass>();

            Mapper.CreateMap<SchoolClass, School.Web.Areas.Administration.Models.SchoolClassDeleteViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear))
                .ForMember(
                    dest => dest.AcademicYearStartDate, 
                    opt => opt.MapFrom(src => src.Grade.AcademicYear.StartDate))
                .ForMember(
                    dest => dest.AcademicYearEndDate,
                    opt => opt.MapFrom(src => src.Grade.AcademicYear.EndDate))
                .ForMember(
                    dest => dest.StudentsNumber,
                    opt => opt.MapFrom(src => src.Students.Count()));
        }
    }
}