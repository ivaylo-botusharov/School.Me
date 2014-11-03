namespace Application.Models
{
    using System;
    using System.Linq;

    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }

        string DeletedBy { get; set; }
    }
}
