using Domain.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure
{
    public class ShippingContext
    {
        public readonly IQueryable<Order> Orders;
        public ShippingContext(IOptions<MongoOptions> options) 
        {
            MongoClient client = new MongoClient(options.Value.Endpoint);
            IMongoDatabase database = client.GetDatabase(options.Value.Database);
            database.CreateCollection(options.Value.OrdersCollection);
            Orders = database.GetCollection<Order>(options.Value.OrdersCollection)
                .AsQueryable();
        }
    }
}
