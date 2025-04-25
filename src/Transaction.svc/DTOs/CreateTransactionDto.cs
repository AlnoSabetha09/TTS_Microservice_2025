using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.svc.DTOs
{
    public class CreateTransactionDto
    {
        public Guid CustomerId { get; set; }
        public List<CreateSaleItemDto> Items { get; set; }
    }

    public class CreateSaleItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}