namespace Application.Services.Interfaces
{
    using Application.Data;
    using Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAcademicYearService : IRepositoryService<AcademicYear>
    {
        UnitOfWork UnitOfWork { get; }
    }
}
