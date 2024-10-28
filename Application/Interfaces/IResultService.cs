using Domain.Models.Entities;

namespace Application.Interfaces
{
    public interface IResultService
    {
        public Task<List<Order>> GetResultAsync(DateTime name, CancellationToken cancellationToken);
        public Task<List<string>> GetResultsNamesAsync();
        public Task<DateTime> CreateResultAsync(string district, CancellationToken cancellationToken);
        public Task DeleteResultAsync(DateTime name, CancellationToken cancellationToken);
    }
}
