using Application.Interfaces;
using Application.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.GetOrderAsync(id, cancellationToken));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetOrders(OrdersFilters filters, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.GetOrdersAsync(filters, cancellationToken));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOrdersInDistrictCount(string district, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.OrdersInDistrictCountAsync(district, cancellationToken));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrder(OrderInputModel order, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.CreateAsync(order, cancellationToken));
        }

        [HttpPatch]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderInputModel order, CancellationToken cancellationToken)
        {
            await _orderService.UpdateAsync(id, order, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            await _orderService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
