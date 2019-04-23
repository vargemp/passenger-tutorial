using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Api.Controllers
{
    public class DriverController : ApiControllerBase
    {
        public DriverController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }

    }
}
