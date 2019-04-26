using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, 
            IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(Guid userId, string name, double startLat, double startLong, double endLat, double endLong)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with {userId} wasnt found");
            }

            var start = Node.Create("Start address", startLong, startLat);
            var end = Node.Create("End address", endLong, endLat);
            driver.AddRoute(name, start, end, new double());
            await _driverRepository.UpdateAsync(driver);

        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with {userId} wasnt found");
            }

            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
