using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.svc.DTOs
{
    public class CreateCustomerDto
    {
        public string CustomerName { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}