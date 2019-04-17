﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<UserDTO> Get(string email)
        {
            return await _userService.GetAsync(email);
        }

        [HttpPost("")]
        public async Task Post([FromBody] CreateUser request)
        {
            await _userService.RegisterAsync(request.Email, request.Username, request.Password);
        }
    }

}
