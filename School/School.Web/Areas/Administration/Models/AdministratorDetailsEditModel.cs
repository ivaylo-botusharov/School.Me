namespace School.Web.Areas.Administration.Models
{
    using System;

    public class AdministratorDetailsEditModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AccountDetailsEditModel AccountDetailsEditModel { get; set; }
    }
}