using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryApp.Models.DTOs
{
    public class PostCategoryRequest
    {

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [JsonPropertyName("name")] // this is where Model Binding will see the link between json body request
        public string NewCategoryName { get; set; }

        public string? Description { get; set; }

    }
}
