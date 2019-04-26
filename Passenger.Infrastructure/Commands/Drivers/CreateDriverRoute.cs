using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriverRoute : AuthenticatedCommandBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public double StartLat { get; set; }
        public double StartLong { get; set; }
        public double EndLat { get; set; }
        public double EndLong { get; set; }
    }
}
