namespace School.Web.AutoMapperConfig
{
    using AutoMapper;
    using School.Models;
    using System.Linq;

    public class OrganizationProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Student, School.Web.Areas.Administration.Models.StudentListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<Administrator, School.Web.Areas.Administration.Models.AdministratorDetailsEditModel>()
                .ForMember(dest => dest.AccountDetailsEditModel, opt => opt.MapFrom(src => src.ApplicationUser));

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AdministratorDetailsEditModel, Administrator>()
                .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => src.AccountDetailsEditModel));

            Mapper.CreateMap<Administrator, School.Web.Areas.Administration.Models.AdministratorListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));


            Mapper.CreateMap<School.Web.Areas.Administration.Models.AdministratorListViewModel, Administrator>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.StudentListViewModel, Student>();

            Mapper.CreateMap<Student, School.Web.Areas.Administration.Models.StudentDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.StudentDetailsEditModel, Student>();

            Mapper.CreateMap<School.Web.Areas.Students.Models.StudentRegisterSubmitModel, Student>();

            Mapper.CreateMap<School.Web.Areas.Teachers.Models.TeacherRegisterSubmitModel, Teacher>();

            Mapper.CreateMap<Teacher, School.Web.Areas.Administration.Models.TeacherListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<Teacher, School.Web.Areas.Administration.Models.TeacherDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.TeacherDetailsEditModel, Teacher>();

            Mapper.CreateMap<ApplicationUser, School.Web.Areas.Administration.Models.AccountDetailsEditModel>();

            Mapper.CreateMap<School.Web.Areas.Administration.Models.AccountDetailsEditModel, ApplicationUser>();

            Mapper.CreateMap<AcademicYear, School.Web.Areas.Administration.Models.AcademicYearListViewModel>();

            Mapper.CreateMap<AcademicYear, School.Web.Areas.Administration.Models.AcademicYearDetailsViewModel>();

            Mapper.CreateMap<Grade, School.Web.Areas.Administration.Models.GradeListViewModel>()
                .ForMember(dest => dest.SchoolClassesCount, opt => opt.MapFrom(src => src.SchoolClasses.Count))
                .ForMember(dest => dest.SchoolClasses, opt => opt.MapFrom(src => src.SchoolClasses.OrderBy(sc => sc.ClassLetter)));

            Mapper.CreateMap<SchoolClass, School.Web.Areas.Administration.Models.SchoolClassListViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear));

            Mapper.CreateMap<SchoolClass, School.Web.Areas.Administration.Models.SchoolClassDetailsViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear))
                .ForMember(dest => dest.StudentsNumber, opt => opt.MapFrom(src => src.Students.Where(s => s.IsDeleted == false).Count()))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Where(s => s.IsDeleted == false)))
                .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src => src.Grade.AcademicYear));
        }

        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
}