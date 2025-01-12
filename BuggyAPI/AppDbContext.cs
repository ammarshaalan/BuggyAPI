using BuggyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BuggyAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
