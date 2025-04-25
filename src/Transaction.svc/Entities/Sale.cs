using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.svc.Entities
{
    public class Sale
    {
        public Guid SaleId { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; }
    }
}