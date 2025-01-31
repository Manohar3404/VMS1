namespace VMS1.Models.ViewModels
{
    public class FeedbackVM
    {
        public Feedback Feedback { get; set; }
        public Event Event { get; set; }    
        public ApplicationUser User { get; set; }
    }
}
