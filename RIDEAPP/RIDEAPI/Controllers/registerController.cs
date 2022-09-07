using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RIDEAPI.Model;
using RIDEAPI.Services;

namespace RIDEAPI.Controllers
{
    [Route("api/v1.0/rideapp/[controller]")]
    [ApiController]
    public class registerController : ControllerBase
    {
        private readonly RideServices _rideservices;
        public registerController(RideServices rideServices)
        {
            this._rideservices = rideServices;
        }

        [HttpGet]
        public async Task<List<Rides>> Get()
        {
            return await _rideservices.GettheRide();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Rides>> GetbyId(string id)
        {
            var ride = await _rideservices.GetById(id);
            if (ride is null)
                return NotFound();

            return ride;
        }

        [HttpPost]
        public async Task<OkObjectResult> Insert(Rides rides)
        {
            await _rideservices.InsertRideDetails(rides);
            return Ok(rides);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Rides rides)
        {
            var existingitem = await _rideservices.GetById(id);

            if (existingitem is null)
            {
                return NotFound();
            }
            rides.Id = existingitem.Id;
            await _rideservices.UpdateRideDetails(id, rides);
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var existingitem = await _rideservices.GetById(id);

            if (existingitem is null)
            {
                return NotFound();
            }
            await _rideservices.DeleteRideDetails(id);
            return NoContent();
        }
    }
}
