using System;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }
        public Node StartNode { get; protected set; }
        public Node EndNode { get; protected set; }
        public double Distance { get; protected set; }

        protected Route()
        {
           
        }

        protected Route(string name, Node startnode, Node endnode, double distance)
        {
            Name = name;
            StartNode = startnode;
            EndNode = endnode;
            Distance = distance;
        }

        public static Route Create(string name, Node startnode, Node endnode, double distance)
            => new Route(name, startnode, endnode, distance);
    }
}
