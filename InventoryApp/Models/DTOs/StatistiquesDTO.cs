namespace InventoryApp.Models.DTOs
{
    public class StatistiquesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeCount { get; set; }
        public int TotalQT { get; set; }
        public int InStock { get; set; }
        public DateTime? LastUpdate { get; set; }
        public decimal TotalValue { get; set; }

    }
}
