namespace School.Web.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (DateTime)value >= DateTime.Now;
        }
    }
}