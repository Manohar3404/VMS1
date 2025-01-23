using VMS1.Data;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Repository
{
    public class VolunteerRegistrationsRepository : Repository<VolunteerRegistrations>, IVolunteerRegistrationsRepository
    {
        private ApplicationDbContext _db;
        public VolunteerRegistrationsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(VolunteerRegistrations obj)
        {
           
           _db.VolunteerRegistrations.Update(obj);
        }

        
    }
}
