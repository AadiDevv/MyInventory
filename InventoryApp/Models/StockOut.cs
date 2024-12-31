using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class StockOut
{
    public int Id { get; set; }

    public DateTime? Timestamp { get; set; }

    public string Type { get; set; } = null!;

    public string? Reason { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<StockOutItem> StockOutItems { get; set; } = new List<StockOutItem>();
}
