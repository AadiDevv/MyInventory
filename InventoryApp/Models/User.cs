using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<StockEntry> StockEntries { get; set; } = new List<StockEntry>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
