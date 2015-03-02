using Application.Data;
using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Application.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();

            //kernel.Bind<IUserStore<ApplicationUser>>()
            //  .To<UserStore<ApplicationUser>>()
            //  .WithConstructorArgument("applicationDbContext", new ApplicationDbContext());

            kernel.Bind<ITeacherService>().To<TeacherService>();
            kernel.Bind<IStudentService>().To<StudentService>();
            kernel.Bind<ICourseService>().To<CourseService>();
        }
    }
}