using VMS1.Models;

namespace VMS1.Repository.IRepository
{
    public interface IVolunteerRegistrationsRepository : IRepository<VolunteerRegistrations>
    {
        void Update(VolunteerRegistrations obj);
        

    }
}

