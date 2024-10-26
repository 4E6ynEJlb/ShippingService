using Domain.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure
{
    public class ShippingContext
    {
        public readonly IQueryable<Order> OrdersQueryable;
        public readonly IMongoCollection<Order> Orders;
        public ShippingContext(IOptions<MongoOptions> options) 
        {
            MongoClient client = new(options.Value.Endpoint);
            IMongoDatabase database = client.GetDatabase(options.Value.Database);
            database.CreateCollection(options.Value.OrdersCollection);
            Orders = database.GetCollection<Order>(options.Value.OrdersCollection);
            Orders.Indexes.CreateOne(new CreateIndexModel<Order>(Builders<Order>.IndexKeys.Ascending(o=>o.DeliveryDateTime)));
            OrdersQueryable = Orders.AsQueryable();
        }
    }
}
