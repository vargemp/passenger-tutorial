using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {
        private IDriverService _driverService;
        public DriversController(IDriverService driverService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new Exception("Ups...");
            var drivers = await _driverService.BrowseAsync();
            return Json(drivers);
        }
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var driver = _driverService.GetAsync(userId);

            if (driver == null)
            {
                return NotFound();
            }

            return Json(driver.Result);
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Post()
        {
            await DispatchAsync(new DeleteDriver());

            return NoContent();
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> Put([FromBody] UpdateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
