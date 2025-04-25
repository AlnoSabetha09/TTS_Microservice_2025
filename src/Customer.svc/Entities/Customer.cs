using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.svc.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}