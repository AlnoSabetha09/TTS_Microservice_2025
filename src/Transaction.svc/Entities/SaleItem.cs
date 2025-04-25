using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.svc.Entities
{
    public class SaleItem
    {
        public Guid SaleItemId { get; set; } = Guid.NewGuid();
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Sale Sale { get; set; }
    }
}