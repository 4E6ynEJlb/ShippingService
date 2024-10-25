using Domain.Models.Entities;
using Domain.Models.ViewModels;

namespace Domain.Stores
{
    public interface IOrderStore
    {
        public Task<Order> GetOrderAsync(Guid orderId);
        public Task<OrdersPageOutput> GetOrdersAsync(OrdersFilters filters);
        public Task<int> OrdersInDistrictCountAsync(string district);
        public Task CreateAsync(Order order);
        public Task UpdateAsync(Order order);
        public Task DeleteAsync(Guid orderId);
    }
}
