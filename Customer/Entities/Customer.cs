using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Customer.Entities;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid CustomerId { get; set; } = Guid.NewGuid();
    public string CustomerName { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
