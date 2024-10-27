using Application.Interfaces;
using Application.Models;
using Domain.Models.Entities;
using Domain.Models.ViewModels;
using Domain.Stores;
using Microsoft.Extensions.Logging;

namespace Application.Service
{
    public class OrderService(IOrderStore orderStore, ILogger<OrderService> logger) : IOrderService
    {
        private readonly IOrderStore _orderStore = orderStore;
        private readonly ILogger<OrderService> _logger = logger;

        public async Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{LoggerMessages.GET_ORDER_MESSAGE}{orderId}");
            orderId.Validate();
            Order result = await _orderStore.GetOrderAsync(orderId, cancellationToken);
            _logger.LogDebug(LoggerMessages.SUCCESS);
            return result;
        }

        public async Task<OrdersPageOutput> GetOrdersAsync(OrdersFilters filters, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{LoggerMessages.GET_ORDER_MESSAGE}{filters}");
            filters.Validate();
            OrdersPageOutput result = await _orderStore.GetOrdersAsync(filters, cancellationToken);
            _logger.LogDebug(LoggerMessages.SUCCESS);
            return result;
        }

        public async Task<int> OrdersInDistrictCountAsync(string district, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{LoggerMessages.GET_DISTRICT_ORDERS_COUNT}{district}");
            district.Validate();
            int result = await _orderStore.OrdersInDistrictCountAsync(district, cancellationToken);
            _logger.LogDebug(LoggerMessages.SUCCESS);
            return result;
        }

        public async Task<Guid> CreateAsync(OrderInputModel orderInputModel, CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();
            Order order = new()
            {
                Id = id,
                District = orderInputModel.District,
                DeliveryDateTime = orderInputModel.DeliveryDateTime,
                Weight = orderInputModel.Weight
            };
            _logger.LogDebug($"{LoggerMessages.CREATE_ORDER}{order}");
            orderInputModel.Validate();
            await _orderStore.CreateAsync(order, cancellationToken);
            _logger.LogDebug(LoggerMessages.SUCCESS);
            return id;
        }

        public async Task DeleteAsync(Guid orderId, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{LoggerMessages.DELETE_ORDER}{orderId}");
            orderId.Validate();
            await _orderStore.DeleteAsync(orderId, cancellationToken);
            _logger.LogDebug(LoggerMessages.SUCCESS);
        }

        public async Task UpdateAsync(Guid orderId, OrderInputModel orderInputModel, CancellationToken cancellationToken)
        {
            Order order = new()
            {
                Id = orderId,
                District = orderInputModel.District,
                DeliveryDateTime = orderInputModel.DeliveryDateTime,
                Weight = orderInputModel.Weight
            };
            _logger.LogDebug($"{LoggerMessages.UPDATE_ORDER}{order}");
            orderId.Validate();
            orderInputModel.Validate();
            await _orderStore.UpdateAsync(order, cancellationToken);
            _logger.LogDebug(LoggerMessages.SUCCESS);
        }
    }
}
