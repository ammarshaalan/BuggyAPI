using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BuggyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShipmentsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetAllShipments()
        {
            var shipments = _context.Shipments.Include(s => s.Customer).ToList(); // Potential performance bottleneck
            return Ok(shipments);
        }

        
        [HttpPost]
        public IActionResult AddShipment(Shipment shipment)
        {
            
            _context.Shipments.Add(shipment);
            _context.SaveChanges(); 

            return CreatedAtAction(nameof(GetShipmentById), new { id = shipment.Id }, shipment);
        }

       
        [HttpGet("{id}")]
        public IActionResult GetShipmentById(int id)
        {
            
            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == id);
            return Ok(shipment); // Null shipment will cause 500 Internal Server Error
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateShipmentStatus(int id, [FromBody] string status)
        {
            
            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == id);

            if (shipment == null)
                return NotFound();

            shipment.Status = status; 
            _context.SaveChanges(); 
            return NoContent();
        }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=localhost;Database=ShipmentsDB;Trusted_Connection=True;");
        }
    }

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
