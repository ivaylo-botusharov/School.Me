namespace School.Data
{
    using School.Data.Repositories;

    public interface IUnitOfWork
    {
        IApplicationUserRepository Users { get; }

        IStudentRepository Students { get; }

        IAdministratorRepository Administrators { get; }

        ISchoolClassRepository SchoolClasses { get; }

        ApplicationDbContext Context { get; }

        void Save();
    }
}