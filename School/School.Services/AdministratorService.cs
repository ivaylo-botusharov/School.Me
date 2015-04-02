namespace School.Services
{
    using School.Data;
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Linq;

    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository administratorRepository;

        private readonly IApplicationUserRepository userRepository;

        //private readonly IApplicationDbContext context;

        public AdministratorService(
            IAdministratorRepository administratorRepository, 
            IApplicationUserRepository userRepository)
        {
            this.administratorRepository = administratorRepository;
            this.userRepository = userRepository;
        }

        public IApplicationUserRepository UserRepository
        {
            get { return this.userRepository; }
        }

        public Administrator GetById(Guid id)
        {
            return this.administratorRepository.GetById(id);
        }

        public Administrator GetByUserName(string username)
        {
            return this.administratorRepository.All().FirstOrDefault(a => a.ApplicationUser.UserName == username);
        }

        public IQueryable<Administrator> All()
        {
            return this.administratorRepository.All();
        }

        public void Add(Administrator administrator)
        {
            this.administratorRepository.Add(administrator);
            this.administratorRepository.SaveChanges();
        }

        public void Update(Administrator administrator)
        {
            this.administratorRepository.Update(administrator);
            this.administratorRepository.SaveChanges();
        }

        public void Delete(Administrator administrator)
        {
            administrator.ApplicationUser.DeletedBy = administrator.DeletedBy;

            this.userRepository.Delete(administrator.ApplicationUser);
            this.administratorRepository.Delete(administrator);
            this.administratorRepository.SaveChanges();
        }

        public bool IsUserNameUniqueOnEdit(Administrator administrator, string username)
        {
            bool isUserNameUnique = !this.administratorRepository.AllWithDeleted()
                .Any(a => (a.ApplicationUser.UserName == username) &&
                    (a.ApplicationUserId != administrator.ApplicationUserId));

            return isUserNameUnique;
        }

        public void Dispose()
        {
            this.administratorRepository.Dispose();
        }
    }
}
