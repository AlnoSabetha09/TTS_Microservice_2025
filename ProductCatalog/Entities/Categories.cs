using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Entities;

[Table("category")]
public class Category
{
    [Key]
    [Column("CategoryId")]
    public Guid CategoryId { get; set; }
    [Column("CategoryName")]
    public string? CategoryName { get; set; }

    public ICollection<Product> Products { get; set; }
}
