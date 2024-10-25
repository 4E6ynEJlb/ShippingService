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
    }
}
