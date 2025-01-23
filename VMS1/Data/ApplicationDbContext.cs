

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VMS1.Models;

namespace VMS1.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
       public DbSet<ApplicationUser> ApplicationUsers { get; set; }
       public DbSet<Event> Events { get; set; }
        public DbSet<VolunteerRegistrations> VolunteerRegistrations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
