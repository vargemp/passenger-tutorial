using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        DriverDTO Get(Guid userId);
    }
}
