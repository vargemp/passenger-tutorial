using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IRouteManager _routeManager;

        public DriverRouteService(IDriverRepository driverRepository, 
            IMapper mapper, IRouteManager routeManager)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _routeManager = routeManager;
        }
        public async Task AddAsync(Guid userId, string name, double startLat, double startLong, double endLat, double endLong)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);

            var startAddress = _routeManager.GetAddressAsync(startLat, startLong);
            var endAddress = _routeManager.GetAddressAsync(endLat, endLong);
            var startNode = Node.Create("Start address", startLong, startLat);
            var endNode = Node.Create("End address", endLong, endLat);
            var distance = _routeManager.CalcLength(startLat, startLong, endLat, endLong);
            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);

        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
