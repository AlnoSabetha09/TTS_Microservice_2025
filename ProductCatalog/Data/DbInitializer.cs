using System;
using ProductCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<ProductCatalogDbContext>());
    }

    private static void SeedData(ProductCatalogDbContext context)
    {
        context.Database.Migrate();

        //categories  init
        if (context.Categories.Any())
        {
            Console.WriteLine("Database has been seeded with categories.");
            return; // DB has been seeded
        }

        var categories = new List<Category>()
        {
            new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Coffee",
            }
        };

        context.AddRange(categories);
        context.SaveChanges();

        //products init
        if (context.Products.Any())
        {
            Console.WriteLine("Database has been seeded with data.");
            return; // DB has been seeded
        }

        var products = new List<Product>()
        {
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Cappucino",
                Price = 20000,
                StockQuantity = 5,
                Description = "A cappuccino is a coffee drink made with espresso, steamed milk, and milk foam",
                CategoryId = categories[0].CategoryId
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Japanese Iced Coffee",
                Price = 25000,
                StockQuantity = 3,
                Description = "Japanese iced coffee is a coffee brewing method that uses hot water to brew coffee directly over ice",
                CategoryId = categories[0].CategoryId
            },
        };

        context.AddRange(products);
        context.SaveChanges();
    }
}
