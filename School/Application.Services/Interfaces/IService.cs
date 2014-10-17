namespace Application.Services.Interfaces
{
    using System;
    using System.Linq;

    public interface IService
    {
        IStudentService Students { get; }

        ICourseService Courses { get; }
    }
}
