using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
namespace EventServices.Models
{
   

    
        public class Event
        {
        [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Description { get; set; }
            public DateOnly Date { get; set; }
        }
    }


