namespace School.Data.Repositories
{
    using School.Models;

    public class MasterScheduleRepository : DeletableEntityRepository<MasterSchedule>, IMasterScheduleRepository
    {
        public MasterScheduleRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
