namespace School.Web.Infrastructure
{
    using Ninject;
    using Ninject.Web.Common;
    using School.Data;
    using School.Services;
    using School.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

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
            kernel.Bind<IAcademicYearService>().To<AcademicYearService>();
            kernel.Bind<ISchoolClassService>().To<SchoolClassService>();
        }
    }
}