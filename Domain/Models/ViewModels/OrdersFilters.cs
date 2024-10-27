namespace Domain.Models.ViewModels
{
    public class OrdersFilters
    {
        public string? District { get; set; }
        public DateTime? MinimalDateTime { get; set; }
        public DateTime? MaximalDateTime { get; set; }
        public bool IsAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public override string ToString()
        {
            return $"District: {District??""}, MinDateTime: {MinimalDateTime}, MaxDateTime: {MaximalDateTime}, Asc: {IsAscending}, Pg: {Page}, PgSize: {PageSize}";
        }
    }
}
