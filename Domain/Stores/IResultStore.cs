using Domain.Models.Entities;

namespace Domain.Stores
{
    public interface IResultStore
    {
        public Task<List<Order>> GetResultAsync(DateTime name, CancellationToken cancellationToken);
        public Task<List<string>> GetResultsNames();
        public Task<DateTime> CreateResultAsync(string district, CancellationToken cancellationToken);
        public Task DeleteResultAsync(DateTime name, CancellationToken cancellationToken);
    }
}
