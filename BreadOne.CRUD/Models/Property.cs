using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreadOne.CRUD.Models
{
    [Table("Property")]
    public class Property
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool Active { get; set; }

    }
}
