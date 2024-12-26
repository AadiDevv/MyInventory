using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class StockOutItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int? StockOutId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual StockOut? StockOut { get; set; }
}
