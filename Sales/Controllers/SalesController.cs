using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SalesService.Data;
using SalesService.DTOs;
using SalesService.Entities;
using SalesService.Services;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SalesDbContext _dbContext;
        private readonly CustomerServiceClient _customerService;
        private readonly ProductServiceClient _productService;

        public SalesController(SalesDbContext dbContext, CustomerServiceClient customerService, ProductServiceClient productService)
        {
            _dbContext = dbContext;
            _customerService = customerService;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(SaleDto dto)
        {
            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                Items = dto.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList(),
                TotalAmount = dto.Items.Sum(i => i.Quantity * i.Price)
            };

            await _dbContext.Sales.InsertOneAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.SaleId }, sale);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SaleResponseDto>> GetSaleById(Guid id)
        {
            var sale = await _dbContext.Sales.Find(s => s.SaleId == id).FirstOrDefaultAsync();
            if (sale == null) return NotFound();

            var customerName = await _customerService.GetCustomerNameAsync(sale.CustomerId);
            var items = new List<SaleItemDetailDto>();

            foreach (var item in sale.Items)
            {
                var productName = await _productService.GetProductNameAsync(item.ProductId);
                items.Add(new SaleItemDetailDto
                {
                    ProductName = productName,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            return Ok(new SaleResponseDto
            {
                SaleId = sale.SaleId,
                CustomerName = customerName,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                Items = items
            });
        }
    }
}
