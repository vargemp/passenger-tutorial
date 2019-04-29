using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class DeleteDriverRoute : AuthenticatedCommandBase
    {
        public string Name { get; set; }
    }
}
