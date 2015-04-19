namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class AdministratorDeleteSubmitModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AccountDetailsEditModel AccountDetailsEditModel { get; set; }

        [Display(Name = "Delete permanently")]
        public bool DeletePermanent { get; set; }
    }
}