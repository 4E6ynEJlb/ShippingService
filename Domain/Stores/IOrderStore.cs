using Domain.Models.Entities;
using Domain.Models.ViewModels;

namespace Domain.Stores
{
    public interface IOrderStore
    {
        public Task<Order?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);
        public Task<OrdersPageOutput> GetOrdersAsync(OrdersFilters filters, CancellationToken cancellationToken);
        public Task<int> OrdersInDistrictCountAsync(string district, CancellationToken cancellationToken);
        public Task CreateAsync(Order order, CancellationToken cancellationToken);
        public Task UpdateAsync(Order order, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
