using Microsoft.AspNetCore.Identity;

namespace VMS1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }  
        public string? City { get; set; } 
        public int? RoleSpecifier { get; set; }
        
    }
}
