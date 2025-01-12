using Microsoft.AspNetCore.Mvc;
using BuggyAPI.Services;
using BuggyAPI.Models;

namespace BuggyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentsController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShipments(int pageNumber = 1, int pageSize = 10)
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync(pageNumber, pageSize);
            return Ok(shipments);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null) return NotFound();

            return Ok(shipment);
        }

        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] Shipment shipment)
        {
            var newShipment = await _shipmentService.AddShipmentAsync(shipment);
            return CreatedAtAction(nameof(GetShipmentById), new { id = newShipment.Id }, newShipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipmentStatus(int id, [FromBody] string status)
        {
            var result = await  _shipmentService.UpdateShipmentStatusAsync(id, status);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
