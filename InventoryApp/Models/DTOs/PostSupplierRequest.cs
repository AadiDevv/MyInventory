using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryApp.Models.DTOs
{
    public class PostSupplierRequest
    {

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [JsonPropertyName("name")] // this is where Model Binding will see the link between json body request
        public string NewSupplierName { get; set; }

        [Required]
        public int? ProductTypeId { get; set; }

        public string? ContactName { get; set; }

        [Phone] // Check if the format of phone is correct
        public string? Phone { get; set; }

        [EmailAddress] // Check if the format of Email is correct
        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
