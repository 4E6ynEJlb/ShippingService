using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Entities
{
    public class Order
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonElement]
        public double Weight { get; set; }
        [BsonElement]
        public required string District { get; set; }
        [BsonElement]
        public DateTime DeliveryDateTime { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Weight: {Weight}, District: {District}, DeliveryDateTime: {DeliveryDateTime}";
        }
    }
}
