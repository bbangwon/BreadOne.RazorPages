using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BreadOne.Website.Models;

namespace BreadOne.Website.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BreadOne.Website.Models.Blog>? Blog { get; set; }
        public DbSet<BreadOne.Website.Models.Post>? Post { get; set; }
    }
}