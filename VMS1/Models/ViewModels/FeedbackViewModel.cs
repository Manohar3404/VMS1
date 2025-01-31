using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VMS1.Models.ViewModels
{
    public class FeedbackViewModel
    {
        public Feedback Feedback { get; set; }
        [ValidateNever]
        public VolunteerRegistrations VolunteerRegistrations { get; set; }
    }
}
