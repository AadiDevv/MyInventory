using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? ProductCount { get; set; }

    public int? ProductTypeId { get; set; }

    public int? DistinctProductCount { get; set; }

    public int? TotalQuantity { get; set; }

    public decimal? TotalValue { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ProductType? ProductType { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
