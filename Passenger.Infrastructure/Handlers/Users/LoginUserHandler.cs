﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class LoginUserHandler : ICommandHandler<LoginUser>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginUserHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache memoryCache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = memoryCache;
        }
        public async Task HandleAsync(LoginUser command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
            _cache.Set(command.TokenId, jwt);
        }
    }
}
