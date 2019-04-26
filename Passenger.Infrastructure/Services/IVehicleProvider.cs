using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IVehicleProvider : IService
    {
        Task<IEnumerable<VehicleDTO>> BrowseAsync();
        Task<VehicleDTO> GetAsync(string brand, string name);
        Task<IEnumerable<VehicleDTO>> GetAllAsync();
    }
}
