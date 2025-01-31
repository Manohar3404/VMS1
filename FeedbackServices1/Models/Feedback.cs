namespace FeedbackServices1.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int EventId { get; set; } 
        public string VolunteerId { get; set; }
      
        public string FeedbackText { get; set; }
        public int Rating { get; set; }
        public DateOnly CreatedAt { get; set; }

    }
}