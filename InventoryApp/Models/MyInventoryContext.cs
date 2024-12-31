using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Models;

public partial class MyInventoryContext : DbContext
{
    public MyInventoryContext()
    {
    }

    public MyInventoryContext(DbContextOptions<MyInventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<StockEntry> StockEntries { get; set; }

    public virtual DbSet<StockEntryItem> StockEntryItems { get; set; }

    public virtual DbSet<StockOut> StockOuts { get; set; }

    public virtual DbSet<StockOutItem> StockOutItems { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PCAADI;Database=MyInventory;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07962434F4");

            entity.Property(e => e.DistinctProductCount).HasDefaultValue(0);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.ProductCount).HasDefaultValue(0);
            entity.Property(e => e.TotalQuantity).HasDefaultValue(0);
            entity.Property(e => e.TotalValue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK_Categories_ProductType");

            entity.HasOne(d => d.User).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Categories_UserId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07F6A6E60B");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PricePurchase).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Reference).HasMaxLength(250);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("active");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__412EB0B6");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK_Product_ProductType");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Products__Suppli__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.Products)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Products_UserId");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductT__3214EC07EB8B96EA");

            entity.ToTable("ProductType");

            entity.HasIndex(e => e.UserId, "IX_ProductType_UserId");

            entity.Property(e => e.DistinctProductCount).HasDefaultValue(0);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TotalQuantity).HasDefaultValue(0);
            entity.Property(e => e.TotalValue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.ProductTypes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ProductType_User");
        });

        modelBuilder.Entity<StockEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockEnt__3214EC07322A570C");

            entity.ToTable("StockEntry");

            entity.Property(e => e.EntryDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockEntries)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__StockEntr__Suppl__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.StockEntries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_StockEntry_UserId");
        });

        modelBuilder.Entity<StockEntryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockEnt__3214EC0726699BD7");

            entity.ToTable("StockEntryItem");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.StockEntryItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__StockEntr__Produ__4CA06362");

            entity.HasOne(d => d.StockEntry).WithMany(p => p.StockEntryItems)
                .HasForeignKey(d => d.StockEntryId)
                .HasConstraintName("FK__StockEntr__Stock__4BAC3F29");
        });

        modelBuilder.Entity<StockOut>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockOut__3214EC0769F7A742");

            entity.ToTable("StockOut");

            entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<StockOutItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockOut__3214EC0760B6D512");

            entity.ToTable("StockOutItem");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.StockOutItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__StockOutI__Produ__5441852A");

            entity.HasOne(d => d.StockOut).WithMany(p => p.StockOutItems)
                .HasForeignKey(d => d.StockOutId)
                .HasConstraintName("FK__StockOutI__Stock__534D60F1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC077F92AD6E");

            entity.Property(e => e.ContactName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.ProductType).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK_Supplier_ProductType");

            entity.HasOne(d => d.User).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Suppliers_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users_Te__3214EC07D61330EF");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
