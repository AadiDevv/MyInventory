using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? ProductTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ProductType? ProductType { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<StockEntry> StockEntries { get; set; } = new List<StockEntry>();
}
