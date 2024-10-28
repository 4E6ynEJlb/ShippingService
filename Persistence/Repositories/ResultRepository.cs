using Domain.Models.Entities;
using Domain.Stores;
using Infrastructure;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Exceptions;

namespace Persistence.Repositories
{
    public class ResultRepository(ShippingContext context) : IResultStore
    {
        private readonly ShippingContext _context = context;

        public async Task<List<Order>> GetResultAsync(DateTime name, CancellationToken cancellationToken)
        {
            if (_context.ResultsCollections.Any(n => n == name.ToString()))
                return await _context.Database.GetCollection<Order>(name.ToString()).AsQueryable().ToListAsync(cancellationToken);
            throw new RecordNotFoundException();
        }

        public Task<List<string>> GetResultsNames()
        {
            return Task.FromResult(_context.ResultsCollections);
        }

        public async Task<DateTime> CreateResultAsync(string district, CancellationToken cancellationToken)
        {
            DateTime firstOrderDateTime = await _context.OrdersQueryable.MinAsync(o => o.DeliveryDateTime);
            DateTime tresholdDateTime = firstOrderDateTime.AddMinutes(30);
            List<Order> orders = await _context.OrdersQueryable.Where(o => o.District == district && o.DeliveryDateTime <= tresholdDateTime).ToListAsync(cancellationToken);
            if (!orders.Any())
                throw new RecordNotFoundException();
            DateTime collectionName = DateTime.Now;
            await _context.Database.GetCollection<Order>(collectionName.ToString()).InsertManyAsync(orders, cancellationToken: cancellationToken);
            return collectionName;
        }

        public async Task DeleteResultAsync(DateTime name, CancellationToken cancellationToken)
        {
            if (!_context.ResultsCollections.Any(n => n == name.ToString()))
                throw new RecordNotFoundException();
            await _context.Database.DropCollectionAsync(name.ToString(), cancellationToken);
        }
    }
}
