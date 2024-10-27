namespace Application.Models
{
    public class OrderInputModel
    {
        public double Weight { get; set; }
        public required string District { get; set; }
        public DateTime DeliveryDateTime { get; set; }
    }
}
