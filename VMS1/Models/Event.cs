using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace VMS1.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
    }
}
