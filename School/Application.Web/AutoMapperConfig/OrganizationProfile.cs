namespace Application.Web.AutoMapperConfig
{
    using System;
    using System.Linq;
    using Application.Models;
    using AutoMapper;

    public class OrganizationProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Student, Application.Web.Areas.Administration.Models.StudentListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<Application.Web.Areas.Administration.Models.StudentListViewModel, Student>();

            Mapper.CreateMap<Student, Application.Web.Areas.Administration.Models.StudentDetailsEditModel>();

            Mapper.CreateMap<Application.Web.Areas.Administration.Models.StudentDetailsEditModel, Student>();

            Mapper.CreateMap<Application.Web.Areas.Students.Models.StudentRegisterSubmitModel, Student>();

            Mapper.CreateMap<Application.Web.Areas.Teachers.Models.TeacherRegisterSubmitModel, Teacher>();

            Mapper.CreateMap<Teacher, Application.Web.Areas.Administration.Models.TeacherListViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));

            Mapper.CreateMap<Teacher, Application.Web.Areas.Administration.Models.TeacherDetailsEditModel>();

            Mapper.CreateMap<Application.Web.Areas.Administration.Models.TeacherDetailsEditModel, Teacher>();

            Mapper.CreateMap<ApplicationUser, Application.Web.Areas.Administration.Models.AccountDetailsEditModel>();

            Mapper.CreateMap<Application.Web.Areas.Administration.Models.AccountDetailsEditModel, ApplicationUser>();

            Mapper.CreateMap<AcademicYear, Application.Web.Areas.Administration.Models.AcademicYearListViewModel>();

            Mapper.CreateMap<AcademicYear, Application.Web.Areas.Administration.Models.AcademicYearDetailsViewModel>();

            Mapper.CreateMap<Grade, Application.Web.Areas.Administration.Models.GradeListViewModel>()
                .ForMember(dest => dest.SchoolClassesCount, opt => opt.MapFrom(src => src.SchoolClasses.Count));

            Mapper.CreateMap<SchoolClass, Application.Web.Areas.Administration.Models.SchoolClassListViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear));
                //.ForMember(dest => dest.SchoolThemeName, opt => opt.MapFrom(src => src.SchoolTheme.Name));

            Mapper.CreateMap<SchoolClass, Application.Web.Areas.Administration.Models.SchoolClassDetailsViewModel>()
                .ForMember(dest => dest.GradeYear, opt => opt.MapFrom(src => src.Grade.GradeYear))
                .ForMember(dest => dest.StudentsNumber, opt => opt.MapFrom(src => src.Students.Where(s => s.IsDeleted == false).Count()))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Where(s => s.IsDeleted == false)))
                .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src => src.Grade.AcademicYear));
                //.ForMember(dest => dest.SchoolThemeName, opt => opt.MapFrom(src => src.SchoolTheme.Name));
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