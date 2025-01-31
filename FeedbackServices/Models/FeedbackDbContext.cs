using Microsoft.EntityFrameworkCore;

namespace FeedbackServices.Models
{
    public class FeedbackDbContext : DbContext
    {
        public DbSet<Feedback> Feedbacks { get; set; }
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }
    }
}
