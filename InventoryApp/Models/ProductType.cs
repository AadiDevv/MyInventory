using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class ProductType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DistinctProductCount { get; set; }

    public int? TotalQuantity { get; set; }

    public decimal? TotalValue { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public virtual User? User { get; set; }
}
