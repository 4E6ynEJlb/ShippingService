using Domain.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure
{
    public class ShippingContext
    {
        public readonly IQueryable<Order> OrdersQueryable;
        public readonly IMongoCollection<Order> Orders;
        public readonly IMongoDatabase Database;
        public List<string> ResultsCollections { 
            get 
            {
                List<string> collections = [];
                foreach (BsonDocument name in Database.ListCollections().ToList())
                {
                    if (name["name"].ToString() == _ordersCollectionName)
                        continue;
                    collections.Add(name["name"].ToString()!);
                }
                return collections; 
            }
        }
        private readonly string _ordersCollectionName;
        public ShippingContext(IOptions<MongoOptions> options) 
        {
            MongoClient client = new(options.Value.Endpoint);
            Database = client.GetDatabase(options.Value.Database);
            _ordersCollectionName = options.Value.OrdersCollection;
            Orders = Database.GetCollection<Order>(_ordersCollectionName);
            Orders.Indexes.CreateOne(new CreateIndexModel<Order>(Builders<Order>.IndexKeys.Ascending(o=>o.DeliveryDateTime)));
            OrdersQueryable = Orders.AsQueryable();
        }
    }
}
