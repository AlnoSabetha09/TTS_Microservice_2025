using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.svc.Data;
using Catalog.svc.DTOs;
using Catalog.svc.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.svc.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogControllers : ControllerBase
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;

        public CatalogControllers(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            if (products == null || !products.Any()) return NotFound("No products found.");

            return _mapper.Map<List<ProductDto>>(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto productDto)
        {
            if (productDto == null) return BadRequest("Invalid product data.");

            var category = await _context.Categories.FindAsync(productDto.CategoryId);
            if (category == null) return BadRequest("Category not found.");

            var product = _mapper.Map<Product>(productDto);
            product.ProductId = Guid.NewGuid();

            _context.Products.Add(product);
            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Problem saving changes.");

            var productDtoResult = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, productDtoResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound($"Product with ID {id} not found.");

            return _mapper.Map<ProductDto>(product);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            if (categories == null || !categories.Any()) return NotFound("No categories found.");

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null) return NotFound($"Category with ID {id} not found.");

            return _mapper.Map<CategoryDto>(category);
        }

        [HttpPost("categories")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest("Invalid category data.");

            var category = _mapper.Map<Category>(categoryDto);
            category.CategoryId = Guid.NewGuid();

            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Problem saving changes.");

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, _mapper.Map<CategoryDto>(category));
        }
    }
}