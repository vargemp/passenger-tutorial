﻿using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Infrastructure.Commands.Drivers.Models;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommandBase 
    {
        public DriverVehicle Vehicle { get; set; }
    }
}
