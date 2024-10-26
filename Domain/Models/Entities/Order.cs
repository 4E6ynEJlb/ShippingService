using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Entities
{
    public class Order
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public double Weight { get; set; }
        [BsonElement]
        public required string District { get; set; }
        [BsonElement]
        public DateTime DeliveryDateTime { get; set; }
    }
}
