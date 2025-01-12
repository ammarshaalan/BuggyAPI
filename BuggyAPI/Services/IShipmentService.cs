using BuggyAPI.Models;

namespace BuggyAPI.Services
{
    public interface IShipmentService
    {
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync(int pageNumber, int pageSize);
        Task<Shipment?> GetShipmentByIdAsync(int id);
        Task<Shipment> AddShipmentAsync(Shipment shipment);
        Task<bool> UpdateShipmentStatusAsync(int id, string status);
    }
}
