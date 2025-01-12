namespace BuggyAPI.Models
{
    public enum ShipmentStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Shipment
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
