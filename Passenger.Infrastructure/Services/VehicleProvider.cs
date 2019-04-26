using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class VehicleProvider : IVehicleProvider
    {
        private readonly IMemoryCache _cache;
        private readonly static string CacheKey = "vehicles";

        private static readonly IDictionary<string, IEnumerable<VehicleDetails>> availableVehicles =
            new Dictionary<string, IEnumerable<VehicleDetails>>
            {
                ["Audi"] = new List<VehicleDetails>
                {
                    new VehicleDetails("A6", 4)
                },
                ["BMW"] = new List<VehicleDetails>
                {
                    new VehicleDetails("E36 316i", 4)
                },
                ["Opel"] = new List<VehicleDetails>
                {
                    new VehicleDetails("Asterka Boża", 4)
                }
            };

        public VehicleProvider(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task<IEnumerable<VehicleDTO>> BrowseAsync()
        {
            var vehicles = _cache.Get<IEnumerable<VehicleDTO>>(CacheKey);
            if (vehicles == null)
            {
                Console.WriteLine($"Getting vehicles from database.");
                vehicles = await GetAllAsync();
                _cache.Set(CacheKey, vehicles);
            }
            else
            {

                Console.WriteLine($"Getting vehicles from cache.");
            }

            return vehicles;
        }

        public async Task<IEnumerable<VehicleDTO>> GetAllAsync()
            => await Task.FromResult(availableVehicles.GroupBy(x => x.Key)
                .SelectMany(g => g.SelectMany(v => v.Value.Select(c => new VehicleDTO
                {
                    Brand = v.Key,
                    Name = c.Name,
                    Seats = c.Seats
                }))));

        public async Task<VehicleDTO> GetAsync(string brand, string name)
        {
            if (!availableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand {brand} is not available");
            }

            var vehicles = availableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);

            if (vehicle == null)
            {
                throw new Exception($"Vehicle {name} for {brand} is not available.");
            }

            return await Task.FromResult(new VehicleDTO
            {
                Brand = brand,
                Name = vehicle.Name,
                Seats = vehicle.Seats
            });
        }

        private class VehicleDetails
        {
            public string Name { get; }
            public int Seats { get; }

            public VehicleDetails(string name, int seats)
            {
                this.Name = name;
                this.Seats = seats;
            }
        }
    }
}
