using Application.Models;
using Domain.Models.Entities;
using Domain.Models.ViewModels;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);
        public Task<OrdersPageOutput> GetOrdersAsync(OrdersFilters filters, CancellationToken cancellationToken);
        public Task<int> OrdersInDistrictCountAsync(string district, CancellationToken cancellationToken);
        public Task<Guid> CreateAsync(OrderInputModel orderInputModel, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid orderId, OrderInputModel orderInputModel, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
