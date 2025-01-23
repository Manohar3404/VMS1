using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMS1.Models
{
    public class VolunteerRegistrations
    {
        [Key]
        public int RegistrationId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string VolunteerId { get; set; }
        [ForeignKey("VolunteerId")]
        public ApplicationUser Volunteer { get; set; }

        public DateOnly RegistrationDate { get; set; }
    }
}
