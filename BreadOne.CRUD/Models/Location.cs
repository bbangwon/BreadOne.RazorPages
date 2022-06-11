using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreadOne.CRUD.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Location Name")]
        public string Name { get; set; } = "";
        public bool Active { get; set; }
        public string? Property { get; set; } = "";

        [Display(Name = "Property")]
        public Property? PropertyRef { get; set; }
        public int? PropertyId { get; set; }
    }
}
