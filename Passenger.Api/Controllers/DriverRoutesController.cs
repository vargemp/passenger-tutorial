using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("drivers/routes")]
    public class DriverRoutesController : ApiControllerBase
    {
        public DriverRoutesController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateDriverRoute command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpDelete("{name}")]
        [Authorize]
        public async Task<IActionResult> Delete(string name)
        {
            var command = new DeleteDriverRoute
            {
                Name = name
            };
            await DispatchAsync(command);
            return NoContent();
        }
    }
}
