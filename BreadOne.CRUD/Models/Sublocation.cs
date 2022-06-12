using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreadOne.CRUD.Models
{
    [Table("Sublocation")]
    public class Sublocation
    {
        public int Id { get; set; }

        [Display(Name = "Sublocation Name")]
        [Column("Sublocation")]
        public string? SublocationName { get; set; }

        public bool Active { get; set; } = false;

        public string? Location { get; set; }

        public string? Property { get; set; }

        [Display(Name = "Location")]
        public Location? LocationRef { get; set; }

        public int? LocationId { get; set; }
    }
}
