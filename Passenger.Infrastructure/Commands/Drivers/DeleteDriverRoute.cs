using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class DeleteDriverRoute : AuthenticatedCommandBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
