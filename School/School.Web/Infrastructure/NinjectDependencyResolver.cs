namespace School.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc; 
    using Ninject;
    using Ninject.Web.Common;
    using School.Data;
    using School.Data.Repositories;
    using School.Services;
    using School.Services.Interfaces;
    
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            this.AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            this.kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();

            /*kernel.Bind<IUserStore<ApplicationUser>>()
               .To<UserStore<ApplicationUser>>()
               .WithConstructorArgument("applicationDbContext", new ApplicationDbContext());*/

            this.kernel.Bind<IAcademicYearRepository>().To<AcademicYearRepository>();
            this.kernel.Bind<IGradeRepository>().To<GradeRepository>();
            this.kernel.Bind<ISchoolClassRepository>().To<SchoolClassRepository>();
            this.kernel.Bind<ISchoolThemeRepository>().To<SchoolThemeRepository>();
            this.kernel.Bind<ISubjectRepository>().To<SubjectRepository>();
            this.kernel.Bind<IAdministratorRepository>().To<AdministratorRepository>();
            this.kernel.Bind<IStudentRepository>().To<StudentRepository>();
            this.kernel.Bind<ITeacherRepository>().To<TeacherRepository>();
            this.kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>();

            this.kernel.Bind<IAcademicYearService>().To<AcademicYearService>();
            this.kernel.Bind<IGradeService>().To<GradeService>();
            this.kernel.Bind<ISchoolClassService>().To<SchoolClassService>();
            this.kernel.Bind<ISchoolThemeService>().To<SchoolThemeService>();
            this.kernel.Bind<ISubjectService>().To<SubjectService>();
            this.kernel.Bind<IAdministratorService>().To<AdministratorService>();
            this.kernel.Bind<IStudentService>().To<StudentService>();
            this.kernel.Bind<ITeacherService>().To<TeacherService>();
        }
    }
}