using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomerService.Data
{
    public class CustomerDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Customer.Entities.Customer> Customers { get; }

        public CustomerDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("CustomerDb");
            Customers = _database.GetCollection<Customer.Entities.Customer>("Customers");

            // Seeding data jika kosong
            if (Customers.CountDocuments(_ => true) == 0)
            {
                Customers.InsertMany(new[]
                {
                    new Customer.Entities.Customer
                    {
                        CustomerName = "John Doe",
                        ContactNumber = "081234567890",
                        Email = "john@example.com",
                        Address = "Jl. Mawar No. 1"
                    },
                    new Customer.Entities.Customer
                    {
                        CustomerName = "Jane Smith",
                        ContactNumber = "089876543210",
                        Email = "jane@example.com",
                        Address = "Jl. Melati No. 2"
                    }
                });
            }
        }
    }
}