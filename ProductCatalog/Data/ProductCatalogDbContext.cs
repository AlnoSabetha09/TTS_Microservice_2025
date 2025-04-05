using System;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Entities;

namespace ProductCatalog.Data;

public class ProductCatalogDbContext : DbContext
{
    public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("category");
        modelBuilder.Entity<Product>().ToTable("product");

        modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
        modelBuilder.Entity<Product>().HasKey(p => p.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId); // Define the foreign key relationship
    }
}
