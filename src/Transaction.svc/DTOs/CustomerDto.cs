using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.svc.DTOs
{
    public class CustomerDto
    {
        public string CustomerName { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}