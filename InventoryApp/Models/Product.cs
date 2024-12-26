using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public string? Status { get; set; }

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public int? UserId { get; set; }

    public string? Reference { get; set; }

    public string? Color { get; set; }

    public int? ProductTypeId { get; set; }

    public decimal? PricePurchase { get; set; }

    public decimal? PriceSale { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ProductType? ProductType { get; set; }

    public virtual ICollection<StockEntryItem> StockEntryItems { get; set; } = new List<StockEntryItem>();

    public virtual ICollection<StockOutItem> StockOutItems { get; set; } = new List<StockOutItem>();

    public virtual Supplier? Supplier { get; set; }

    public virtual User? User { get; set; }
}
