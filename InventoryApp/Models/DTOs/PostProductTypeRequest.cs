using System.Text.Json.Serialization;

namespace InventoryApp.Models.DTOs
{
    public class PostProductTypeRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        public int? UserId { get; set; }
    }
}
