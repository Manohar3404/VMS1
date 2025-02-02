using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace VMS1.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }  
        public string? State { get; set; }
        public string? Address { get; set; }
        
    }
}
