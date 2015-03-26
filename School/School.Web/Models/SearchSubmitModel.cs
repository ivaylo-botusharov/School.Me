namespace School.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SearchSubmitModel
    {
        [Display(Name = "Search:")]
        public string NameSearch { get; set; }
    }
}