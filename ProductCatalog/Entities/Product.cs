using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Entities;

[Table("product")]
public class Product
{
    [Key]
    [Column("ProductId")]
    public Guid ProductId { get; set; }
    [Column("ProductName")]
    public string? ProductName { get; set; }
    [Column("Price")]
    public int Price { get; set; }
    [Column("StockQuantity")]
    public int StockQuantity { get; set; }
    [Column("Description")]
    public string? Description { get; set; }

    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
}
