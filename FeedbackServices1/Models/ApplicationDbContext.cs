using Microsoft.EntityFrameworkCore;

namespace FeedbackServices1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
    }
}

