using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Api.Controllers
{
    [Route("controller")]
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache) 
                : base(commandDispatcher)
        {
            _cache = cache;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] LoginUser request)
        {
            request.TokenId = Guid.NewGuid(); 
            await DispatchAsync(request);
            var jwt = _cache.GetJwt(request.TokenId);

            return Json(jwt);
        }
    }
}
