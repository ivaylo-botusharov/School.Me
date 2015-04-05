namespace School.Services.Interfaces
{
    using System;
    using School.Data.Repositories;
    using School.Models;

    public interface IAdministratorService : IRepositoryService<Administrator>
    {
        IApplicationUserRepository UserRepository { get; }

        Administrator GetById(Guid id);

        Administrator GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Administrator administrator, string username);
    }
}
