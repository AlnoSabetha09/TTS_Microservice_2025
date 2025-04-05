using SalesService.Data;
using SalesService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SalesDbContext>();
builder.Services.AddHttpClient<CustomerServiceClient>(c => c.BaseAddress = new Uri("http://customerservice:5001/"));
builder.Services.AddHttpClient<ProductServiceClient>(c => c.BaseAddress = new Uri("http://productcatalog:5002/"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
