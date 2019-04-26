using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger,
                    IDriverService driverService, IDriverRouteService driverRouteService)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializing data...");

            var task = new List<Task>();
           
            for (int i = 0; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                _logger.LogTrace($"Created new user: {username}.");
                task.Add(_userService.RegisterAsync(userId, $"{username}@test.com",
                                username, "secret", "user"));

                task.Add(_driverService.CreateAsync(userId));
                task.Add(_driverService.SetVehicleAsync(userId, "Audi", "A6"));
                task.Add(_driverRouteService.AddAsync(userId, "defRoute", 1,1,2,2));
                task.Add(_driverRouteService.AddAsync(userId, "jobRoute", 3, 4, 7,8));
                _logger.LogTrace($"Created a new driver for: '{username}'.");
            }

            for (int i = 0; i < 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                _logger.LogTrace($"Created new admin: {username}.");
                task.Add(_userService.RegisterAsync(userId, $"{username}@test.com",
                    username, "secret", "admin"));
            }

            await Task.WhenAll(task);

            _logger.LogTrace("Data was initialized.");


        }
    }
}
