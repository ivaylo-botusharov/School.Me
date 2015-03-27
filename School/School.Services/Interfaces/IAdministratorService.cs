namespace School.Services.Interfaces
{
    using School.Models;
    using System;

    public interface IAdministratorService : IRepositoryService<Administrator>
    {
        Administrator GetById(Guid id);

        Administrator GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Administrator administrator, string username);
    }
}
