using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryApp.Models.DTOs
{
    public class PostProductRequest
    {

        [Required]
        [JsonPropertyName("name")] // this is where Model Binding will see the link between json body request
        public string NewProductName { get; set; }

        public string? Reference { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public decimal? PricePurchase { get; set; }
        public decimal? PriceSale { get; set; }

    }
}
