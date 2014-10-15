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
            Mapper.CreateMap<Student, StudentBasicViewModel>();
            Mapper.CreateMap<Student, StudentDetailsEditModel>();
            Mapper.CreateMap<StudentDetailsEditModel, Student>();
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