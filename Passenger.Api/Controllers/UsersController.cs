using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher,
            GeneralSettings settings) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Get(string email)
        {
            var user =  await _userService.GetAsync(email);

            if (user == null)
            {
                return new NotFoundResult();
            }

            return new JsonResult(user);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateUser request)
        {
            await CommandDispatcher.DispatchAsync(request);
            return new CreatedResult($"users/{request.Email}", new object());
        }
    }

}
