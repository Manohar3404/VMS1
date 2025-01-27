using VMS1.Data;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       

        public void Update(ApplicationUser obj)
        {
           _db.ApplicationUsers.Update(obj);
        }
    }
}
