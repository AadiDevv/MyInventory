using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApp.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [MaxLength(10)]
        public string Type { get; set; } // ex: "sale", "restock"

        public string Reason { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
