using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDetailsDTO : DriverDTO
    {
        public IEnumerable<RouteDTO> Routes { get; set; }
    }
}
