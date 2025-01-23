using VMS1.Data;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Repository
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private ApplicationDbContext _db;
        public FeedbackRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Feedback obj)
        {
           //dbset.Update(obj);
           _db.Feedbacks.Update(obj);
        }

        
    }
}
