using VMS1.Models;

namespace VMS1.Repository.IRepository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        void Update(Feedback obj);
        

    }
}

