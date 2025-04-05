using Customer.DTOs;
using CustomerService.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDbContext _dbContext;

        public CustomersController(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer.Entities.Customer>>> GetAll()
        {
            var customers = await _dbContext.Customers.Find(_ => true).ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Customer.Entities.Customer>> GetById(Guid id)
        {
            var customer = await _dbContext.Customers.Find(c => c.CustomerId == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto dto)
        {
            var newCustomer = new Customer.Entities.Customer
            {
                CustomerName = dto.CustomerName,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email,
                Address = dto.Address
            };

            await _dbContext.Customers.InsertOneAsync(newCustomer);
            return CreatedAtAction(nameof(GetById), new { id = newCustomer.CustomerId }, newCustomer);
        }
    }
}
