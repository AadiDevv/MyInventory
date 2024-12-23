using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public int? OldQuantity { get; set; }

    public int? NewQuantity { get; set; }

    public string Type { get; set; } = null!;

    public string? Reason { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
