using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IRouteManager : IService
    {
        Task<string> GetAddressAsync(double lat, double lon);
        double CalcLength(double startLat, double startLon, double endLat, double endLon);
    }
}
