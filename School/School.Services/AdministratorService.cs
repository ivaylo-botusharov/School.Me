namespace School.Services
{
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Linq;

    public class AdministratorService : IAdministratorService
    {
        private readonly UnitOfWork unitOfWork;

        public AdministratorService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Administrator GetById(Guid id)
        {
            return this.unitOfWork.Administrators.GetById(id);
        }

        public Administrator GetByUserName(string username)
        {
            return this.unitOfWork.Administrators.All().FirstOrDefault(a => a.ApplicationUser.UserName == username);
        }

        public IQueryable<Administrator> All()
        {
            return this.unitOfWork.Administrators.All();
        }

        public void Add(Administrator administrator)
        {
            this.unitOfWork.Administrators.Add(administrator);
            this.unitOfWork.Save();
        }

        public void Update(Administrator administrator)
        {
            this.unitOfWork.Administrators.Update(administrator);
            this.unitOfWork.Save();
        }

        public void Delete(Administrator administrator)
        {
            administrator.ApplicationUser.DeletedBy = administrator.DeletedBy;

            this.unitOfWork.Users.Delete(administrator.ApplicationUser);
            this.unitOfWork.Administrators.Delete(administrator);
            this.unitOfWork.Save();
        }

        public bool IsUserNameUniqueOnEdit(Administrator administrator, string username)
        {
            bool isUserNameUnique = !this.unitOfWork.Administrators.AllWithDeleted()
                .Any(a => (a.ApplicationUser.UserName == username) &&
                    (a.ApplicationUserId != administrator.ApplicationUserId));

            return isUserNameUnique;
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
