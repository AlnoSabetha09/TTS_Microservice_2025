using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.DTOs;
using ProductCatalog.Entities;

namespace ProductCatalog.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ProductCatalogDbContext _context;

    private readonly IMapper _mapper;

    public ProductController(ProductCatalogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductsDto>>> GetProducts()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return _mapper.Map<List<ProductsDto>>(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductsDto>> GetProductById(Guid id)
    {
        var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        return _mapper.Map<ProductsDto>(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, productDto);
    }
}
