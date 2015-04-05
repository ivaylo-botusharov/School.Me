namespace School.Web.Models.AccountViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}