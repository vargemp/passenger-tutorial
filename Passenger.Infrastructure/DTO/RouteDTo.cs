using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.DTO
{
    public class RouteDTO
    {
        public string Name { get; set; }
        public NodeDTO StartNode { get; set; }
        public NodeDTO EndNode { get; set; }
        public double Distance { get; set; }
    }
}
