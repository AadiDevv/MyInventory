using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProductCount { get; set; } = 0;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Relations
        public ICollection<Product> Products { get; set; }
    }
}
