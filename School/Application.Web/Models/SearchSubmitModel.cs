namespace Application.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SearchSubmitModel
    {
        [Display(Name = "Search:")]
        public string NameSearch { get; set; }
    }
}