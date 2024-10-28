using Application.Interfaces;
using Application.Models;
using Domain.Models.Entities;
using Domain.Stores;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ResultService(IResultStore resultStore, ILogger<ResultService> logger) : IResultService
    {
        private readonly ILogger<ResultService> _logger = logger;
        private readonly IResultStore _resultStore = resultStore;

        public async Task<List<Order>> GetResultAsync(DateTime name, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{LoggerMessages.GET_RESULT}{name}");
            name.Validate();
            List<Order> result = await _resultStore.GetResultAsync(name, cancellationToken);
            _logger.LogInformation(LoggerMessages.SUCCESS);
            return result;
        }

        public async Task<List<string>> GetResultsNamesAsync()
        {
            _logger.LogInformation(LoggerMessages.GET_RESULTS_NAMES);
            List<string> result = await _resultStore.GetResultsNames();
            _logger.LogInformation(LoggerMessages.SUCCESS);
            return result;
        }

        public async Task<DateTime> CreateResultAsync(string district, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{LoggerMessages.CREATE_RESULT}{district}");

            district.Validate();
            DateTime result = await _resultStore.CreateResultAsync(district, cancellationToken);
            _logger.LogInformation(LoggerMessages.SUCCESS);
            return result;
        }

        public async Task DeleteResultAsync(DateTime name, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{LoggerMessages.DELETE_RESULT}{name}");

            name.Validate();
            await _resultStore.DeleteResultAsync(name, cancellationToken);
            _logger.LogInformation(LoggerMessages.SUCCESS);
        }
    }
}
