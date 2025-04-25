using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customer.svc.Data;
using Customer.svc.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.svc.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(CustomerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer.svc.Entities.Customer>(customerDto);
            _context.Customers.Add(customer);
            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Problem saving changes");
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, _mapper.Map<CustomerDto>(customer));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}