using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(250)]
    public string Password { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    // Relations
    public ICollection<Category> Categories { get; set; }
    public ICollection<Supplier> Suppliers { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}

