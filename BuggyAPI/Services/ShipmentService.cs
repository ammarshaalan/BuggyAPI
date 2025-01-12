using BuggyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BuggyAPI.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly AppDbContext _context;

        public ShipmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync(int pageNumber, int pageSize)
        {
            return await _context.Shipments
                                  .Include(s => s.Customer)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
        }

        public async Task<Shipment?> GetShipmentByIdAsync(int id)
        {
            return await _context.Shipments.Include(s => s.Customer).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Shipment> AddShipmentAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }

        public async Task<bool> UpdateShipmentStatusAsync(int id, string status)
        {
            var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
            if (shipment == null) return false;

            if (!Enum.TryParse<ShipmentStatus>(status, true, out var parsedStatus))
            {
                return false;  // Invalid status input
            }

            shipment.Status = parsedStatus;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
