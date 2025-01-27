using VMS1.Data;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Repository
{
    public class UnitWork : IUnitWork
    {
     

        public IEventRepository Events { get; private set; }

        public IVolunteerRegistrationsRepository VolunteerRegistrations { get; private set; }
        public IFeedbackRepository Feedback { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IFeedbackRepository Feedbacks { get; private set; }  
        private readonly ApplicationDbContext _db;

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Events = new EventRepository(_db);
            VolunteerRegistrations = new VolunteerRegistrationsRepository(_db);
            Feedback = new FeedbackRepository(_db);
            ApplicationUsers = new ApplicationUserRepository(_db);
            Feedbacks = new FeedbackRepository(_db);

        }
        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
