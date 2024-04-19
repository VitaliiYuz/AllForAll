using BusinessLogic.Dto.Manufacturer;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AllForAll.Controllers
{
    [ApiController]
    [Route("api/manufacturers")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManufacturersAsync(CancellationToken cancellationToken)
        {
            var manufacturers = await _manufacturerService.GetAllManufacturersAsync(cancellationToken);
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturerByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id, cancellationToken);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return Ok(manufacturer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManufacturerAsync([FromBody] ManufacturerRequestDto manufacturer, CancellationToken cancellationToken)
        {
            var manufacturerId = await _manufacturerService.CreateManufacturerAsync(manufacturer, cancellationToken);
            return Ok(manufacturerId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManufacturerAsync([FromRoute] int id, [FromBody] ManufacturerRequestDto manufacturer, CancellationToken cancellationToken)
        {
            await _manufacturerService.UpdateManufacturerAsync(id, manufacturer, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturerAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _manufacturerService.DeleteManufacturerAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
