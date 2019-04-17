using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        Task<DriverDTO> GetAsync(Guid userId);
    }
}
