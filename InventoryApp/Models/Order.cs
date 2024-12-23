using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Order
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Supplier Supplier { get; set; } = null!;
}
