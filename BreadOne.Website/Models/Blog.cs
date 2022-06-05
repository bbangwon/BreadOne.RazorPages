using System.ComponentModel.DataAnnotations.Schema;

namespace BreadOne.Website.Models
{
    [Table("Blogs")]
    public class Blog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
