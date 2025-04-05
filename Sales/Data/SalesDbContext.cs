using MongoDB.Driver;
using SalesService.Entities;

namespace SalesService.Data
{
    public class SalesDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Sale> Sales { get; }

        public SalesDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("SalesDb");
            Sales = _database.GetCollection<Sale>("Sales");
        }
    }
}
