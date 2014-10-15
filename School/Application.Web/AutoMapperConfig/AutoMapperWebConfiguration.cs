namespace Application.Web.AutoMapperConfig
{
    using System;
    using System.Linq;
    using AutoMapper;

    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.AddProfile<OrganizationProfile>();
            });
        }
    }
}