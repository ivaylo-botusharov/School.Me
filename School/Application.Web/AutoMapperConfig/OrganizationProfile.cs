namespace Application.Web.AutoMapperConfig
{
    using System;
    using System.Linq;
    using Application.Models;
    using Application.Web.Models;
    using AutoMapper;

    public class OrganizationProfile : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<Student, StudentBasicViewModel>();
            Mapper.CreateMap<Student, StudentBasicViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName));
           
            Mapper.CreateMap<Student, StudentDetailsEditModel>();
            
            Mapper.CreateMap<StudentDetailsEditModel, Student>();

            Mapper.CreateMap<StudentRegisterSubmitModel, Student>();

            Mapper.CreateMap<AccountDetailsEditModel, ApplicationUser>();

            Mapper.CreateMap<ApplicationUser, AccountDetailsEditModel>();
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