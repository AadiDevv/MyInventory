using System;
using System.Collections.Generic;

namespace InventoryApp.Models;

public partial class StockEntry
{
    public int Id { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? Status { get; set; }

    public int? SupplierId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<StockEntryItem> StockEntryItems { get; set; } = new List<StockEntryItem>();

    public virtual Supplier? Supplier { get; set; }

    public virtual User? User { get; set; }
}
