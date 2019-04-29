using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RouteManager : IRouteManager
    {
        private static readonly Random _rand = new Random();

        public async Task<string> GetAddressAsync(double lat, double lon)
            => await Task.FromResult($"Sample address {_rand.Next(100)}");

        public double CalcLength(double startLat, double startLon, double endLat, double endLon)
            => _rand.Next(500, 10000);

    }
}
