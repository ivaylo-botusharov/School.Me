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