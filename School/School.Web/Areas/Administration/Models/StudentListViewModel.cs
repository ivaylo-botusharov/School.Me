namespace School.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}