using Microsoft.AspNetCore.Identity;

namespace BreadOne.WebApp.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
