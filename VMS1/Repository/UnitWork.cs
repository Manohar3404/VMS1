using VMS1.Data;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Repository
{
    public class UnitWork : IUnitWork
    {
     

        public IEventRepository Events { get; private set; }

        public IVolunteerRegistrationsRepository VolunteerRegistrations { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Events = new EventRepository(_db);
            VolunteerRegistrations = new VolunteerRegistrationsRepository(_db);
        }
        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
