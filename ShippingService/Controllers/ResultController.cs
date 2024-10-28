using Application.Interfaces;
using Application.Models;
using Application.Service;
using Domain.Models.Entities;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для работы с результатами фильтрации
    /// </summary>
    /// <param name="resultService"></param>    
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService = resultService;

        /// <summary>
        /// Эндпоинт для получения результата фильтрации
        /// </summary>
        /// <param name="name">Имя коллекции фильтрованных данных</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат фильтрации</returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<Order>), 200)]
        public async Task<IActionResult> GetResult(DateTime name, CancellationToken cancellationToken)
        {
            return Ok(await _resultService.GetResultAsync(name, cancellationToken));
        }

        /// <summary>
        /// Эндпоинт для получения имен коллекций отфильтрованных данных
        /// </summary>
        /// <returns>Список имен</returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IActionResult> GetResultsNames()
        {
            return Ok(await _resultService.GetResultsNamesAsync());
        }

        /// <summary>
        /// Эндпоинт для сохранения в новой коллекции заказов, которые необходимо доставить за 30 минут после доставки 1 заказа в указанном районе
        /// </summary>
        /// <param name="district">Район</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Имя коллекции</returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DateTime), 200)]
        public async Task<IActionResult> CreateResult(string district, CancellationToken cancellationToken)
        {
            return Ok(await _resultService.CreateResultAsync(district, cancellationToken));
        }

        /// <summary>
        /// Эндпоинт для удаления коллекции результата фильтрации
        /// </summary>
        /// <param name="name">Имя коллекции</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteResult(DateTime name, CancellationToken cancellationToken)
        {
            await _resultService.DeleteResultAsync(name, cancellationToken);
            return Ok();
        }
    }
}
