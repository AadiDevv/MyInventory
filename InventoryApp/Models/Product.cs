using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int? ReorderLevel { get; set; }

        [MaxLength(10)]
        public string Status { get; set; } = "active";

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Relations
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
