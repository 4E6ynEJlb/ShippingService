using Domain.Models.Entities;

namespace Domain.Models.ViewModels
{
    public class OrdersPageOutput
    {
        public required Order[] Orders { get; set; }
        public int PagesCount { get; set; }
    }
}
