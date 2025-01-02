using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class StockEntryItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int? StockEntryId { get; set; }

    public int? ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product? Product { get; set; }

    public virtual StockEntry? StockEntry { get; set; }
}
