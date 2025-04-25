using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.svc.DTOs
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
    }
}