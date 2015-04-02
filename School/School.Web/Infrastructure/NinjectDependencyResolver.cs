namespace School.Web.Infrastructure
{
    using Ninject;
    using Ninject.Web.Common;
    using School.Data;
    using School.Data.Repositories;
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

            kernel.Bind<IAcademicYearRepository>().To<AcademicYearRepository>();
            kernel.Bind<IGradeRepository>().To<GradeRepository>();
            kernel.Bind<ISchoolClassRepository>().To<SchoolClassRepository>();
            kernel.Bind<ISchoolThemeRepository>().To<SchoolThemeRepository>();
            kernel.Bind<ISubjectRepository>().To<SubjectRepository>();
            kernel.Bind<IAdministratorRepository>().To<AdministratorRepository>();
            kernel.Bind<IStudentRepository>().To<StudentRepository>();
            kernel.Bind<ITeacherRepository>().To<TeacherRepository>();
            kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>();

            kernel.Bind<IAcademicYearService>().To<AcademicYearService>();
            kernel.Bind<IGradeService>().To<GradeService>();
            kernel.Bind<ISchoolClassService>().To<SchoolClassService>();
            kernel.Bind<ISchoolThemeService>().To<SchoolThemeService>();
            kernel.Bind<ISubjectService>().To<SubjectService>();
            kernel.Bind<IAdministratorService>().To<AdministratorService>();
            kernel.Bind<IStudentService>().To<StudentService>();
            kernel.Bind<ITeacherService>().To<TeacherService>();
        }
    }
}