using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService : IService
    {
        Task<DriverDetailsDTO> GetAsync(Guid userId);
        Task<IEnumerable<DriverDTO>> BrowseAsync();
        Task CreateAsync(Guid userId);
        Task SetVehicleAsync(Guid userId, string brand, string name);
        Task DeleteAsync(Guid userId);
    }
}
