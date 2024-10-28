using Application.Interfaces;
using Application.Models;
using Domain.Models.Entities;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для работы с исходными данными
    /// </summary>
    /// <param name="orderService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        /// <summary>
        /// Эндпоинт для получения заказа
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Найденный заказ</returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(Order), 200)]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.GetOrderAsync(id, cancellationToken));
        }

        /// <summary>
        /// Эндпоинт для получения страницы заказов, соответствующих критериям поиска
        /// </summary>
        /// <param name="filters">Критерии поиска</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Страница заказов</returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OrdersPageOutput), 200)]
        public async Task<IActionResult> GetOrders(OrdersFilters filters, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.GetOrdersAsync(filters, cancellationToken));
        }

        /// <summary>
        /// Эндпоинт для получения количества заказов в указанном районе
        /// </summary>
        /// <param name="district">Район</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Количество заказов</returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetOrdersInDistrictCount(string district, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.OrdersInDistrictCountAsync(district, cancellationToken));
        }

        /// <summary>
        /// Эндпоинт для создания заказа
        /// </summary>
        /// <param name="order">Данные заказа</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id заказа</returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateOrder(OrderInputModel order, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.CreateAsync(order, cancellationToken));
        }

        /// <summary>
        /// Эндпоинт для редактирования заказа
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <param name="order">Новые данные заказа</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("[action]")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderInputModel order, CancellationToken cancellationToken)
        {
            await _orderService.UpdateAsync(id, order, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Эндпоинт для удаления заказа
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
        {
            await _orderService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
