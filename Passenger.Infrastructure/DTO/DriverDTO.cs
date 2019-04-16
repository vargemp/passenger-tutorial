using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDTO
    {
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public IEnumerable<Route> Routes { get; set; }
        public IEnumerable<DailyRoute> DailyRoutes { get; set; }
    }
}
