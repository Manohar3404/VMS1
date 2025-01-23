using VMS1.Models;

namespace VMS1.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        void Update(Event obj);
        

    }
}

