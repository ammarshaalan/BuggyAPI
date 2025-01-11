using Microsoft.EntityFrameworkCore;

namespace BuggyAPI
{
    public class Shipment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
