using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesService.Entities
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid SaleId { get; set; } = Guid.NewGuid();

        public Guid CustomerId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}
