using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.Services;

namespace TicketingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly VenueService _venueService;

        public VenueController(VenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVenues()
        {
            var venues = await _venueService.GetAllVenuesAsync();
            return Ok(venues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenueById(Guid id)
        {
            var venue = await _venueService.GetVenueByIdAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return Ok(venue);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(Venue newVenue)
        {
            await _venueService.AddVenueAsync(newVenue);
            return CreatedAtAction(nameof(GetVenueById), new { id = newVenue.Id }, newVenue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVenue(Guid id, Venue updatedVenue)
        {
            if (id != updatedVenue.Id)
            {
                return BadRequest();
            }

            await _venueService.UpdateVenueAsync(updatedVenue);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(Guid id)
        {
            await _venueService.DeleteVenueAsync(id);
            return NoContent();
        }

        [HttpGet("{venueId}/sections")]
        public async Task<IActionResult> GetSectionsByVenueId(Guid venueId)
        {
            var venue = await _venueService.GetVenueByIdAsync(venueId);
            if (venue == null)
            {
                return NotFound();
            }
            return Ok(venue.Sections);
        }
    }
}
