using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transaction.svc.Data;
using Transaction.svc.DTOs;
using Transaction.svc.Entities;

namespace Transaction.svc.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionDbContext _context;
        private readonly HttpClient _httpClient;

        public TransactionController(TransactionDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionDto dto)
        {
            var sale = new Sale
            {
                SaleId = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                SaleDate = DateTime.UtcNow,
                TotalAmount = dto.Items.Sum(i => i.Quantity * i.Price)
            };

            sale.SaleItems = dto.Items.Select(i => new SaleItem
            {
                SaleItemId = Guid.NewGuid(),
                SaleId = sale.SaleId,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var transactionDto = new TransactionDto
            {
                SaleId = sale.SaleId,
                SaleDate = sale.SaleDate,
                CustomerName = await GetCustomerName(sale.CustomerId),
                TotalAmount = sale.TotalAmount,
                Items = sale.SaleItems.Select(async i => new TransactionItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = await GetProductName(i.ProductId),
                    Quantity = i.Quantity,
                    Price = i.Price
                }).Select(t => t.Result).ToList()
            };

            return CreatedAtAction(nameof(GetTransactions), new { id = sale.SaleId }, transactionDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactions()
        {
            var saleItems = await _context.SaleItems
                .Include(si => si.Sale)
                .ToListAsync();

            var grouped = saleItems.GroupBy(si => si.SaleId);
            var result = new List<TransactionDto>();

            foreach (var group in grouped)
            {
                var firstItem = group.First();
                var sale = firstItem.Sale;

                var customerName = await GetCustomerName(sale.CustomerId);

                var items = new List<TransactionItemDto>();
                foreach (var item in group)
                {
                    var productName = await GetProductName(item.ProductId);
                    items.Add(new TransactionItemDto
                    {
                        ProductId = item.ProductId,
                        ProductName = productName,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                result.Add(new TransactionDto
                {
                    SaleId = sale.SaleId,
                    SaleDate = sale.SaleDate,
                    CustomerName = customerName,
                    TotalAmount = sale.TotalAmount,
                    Items = items
                });
            }

            return Ok(result);
        }

        private async Task<string> GetCustomerName(Guid customerId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5160/api/customer/{customerId}");
            if (!response.IsSuccessStatusCode) return "Unknown Customer";
            var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();
            return customer?.CustomerName ?? "Unknown Customer";
        }

        private async Task<string> GetProductName(Guid productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5066/api/catalog/{productId}");
            if (!response.IsSuccessStatusCode) return "Unknown Product";
            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            return product?.ProductName ?? "Unknown Product";
        }
    }
}