using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands.Drivers.Models;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class UpdateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle { get; set; } 

    }
}
