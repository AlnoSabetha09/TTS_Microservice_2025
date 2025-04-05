using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

builder.Services.AddDbContext<ProductCatalogDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("ProductCatalogDb"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing database: {ex.Message}");
    throw;
}

app.Run();
