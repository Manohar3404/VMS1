using VMS1.Models;

namespace VMS1.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);
        

    }
}

