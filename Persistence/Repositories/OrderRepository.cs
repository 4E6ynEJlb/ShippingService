using Domain.Models.Entities;
using Domain.Models.ViewModels;
using Domain.Stores;
using Infrastructure;
using Persistence.Exceptions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Domain.Models;

namespace Persistence.Repositories
{
    public class OrderRepository(ShippingContext context) : IOrderStore
    {
        private readonly ShippingContext _context = context;

        public async Task<Order?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _context.OrdersQueryable.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }

        public async Task<OrdersPageOutput> GetOrdersAsync(OrdersFilters filters, CancellationToken cancellationToken)
        {
            IQueryable<Order> query = _context.OrdersQueryable;

            if (filters.MinimalDateTime.HasValue)
                query = query.Where(o => o.DeliveryDateTime >= filters.MinimalDateTime.Value);

            if (filters.MaximalDateTime.HasValue)
                query = query.Where(o => o.DeliveryDateTime <= filters.MaximalDateTime.Value);

            if (filters.District != null && filters.District != String.Empty)
                query = query.Where(o => o.District == filters.District);

            query = filters.IsAscending ? query.OrderBy(o => o.DeliveryDateTime) :
                query.OrderByDescending(o => o.DeliveryDateTime);

            int ordersCount = await query.CountAsync(cancellationToken);
            int pagesCount = 1;
            if (ordersCount > 0)
                pagesCount = ordersCount / filters.PageSize +
                    ((ordersCount % filters.PageSize) == 0 ? 0 : 1);

            if (filters.Page > pagesCount || filters.Page < 1)
                throw new InvalidPageNumberException();
            query = query.Skip((filters.Page - 1) * filters.PageSize).Take(filters.PageSize);
            return new() 
            { 
                Orders = await query.ToListAsync(cancellationToken), 
                PagesCount = pagesCount 
            };
        }

        public async Task<int> OrdersInDistrictCountAsync(string district, CancellationToken cancellationToken)
        {
            return await _context.OrdersQueryable.Where(o => o.District == district).CountAsync(cancellationToken);
        }

        public async Task CreateAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.InsertOneAsync(order, new(), cancellationToken);
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.Id, order.Id);
            UpdateDefinition<Order> update = Builders<Order>.Update
                .Set(o => o.District, order.District)
                .Set(o => o.Weight, order.Weight)
                .Set(o => o.DeliveryDateTime, order.DeliveryDateTime);
            var res = await _context.Orders.UpdateOneAsync(filter, update, new(), cancellationToken);
            if (!res.IsAcknowledged)
                throw new Exception(ErrorsMessages.UPDATE_ERROR);
            if (res.ModifiedCount == 0)
                throw new RecordNotFoundException();
        }

        public async Task DeleteAsync(Guid orderId, CancellationToken cancellationToken)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            var res = await _context.Orders.DeleteOneAsync(filter, cancellationToken);
            if (!res.IsAcknowledged)
                throw new Exception(ErrorsMessages.DELETION_ERROR);
            if (res.DeletedCount == 0)
                throw new RecordNotFoundException();
        }
    }
}
