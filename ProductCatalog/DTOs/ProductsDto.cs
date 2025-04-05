using System;

namespace ProductCatalog.DTOs;

public class ProductsDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
