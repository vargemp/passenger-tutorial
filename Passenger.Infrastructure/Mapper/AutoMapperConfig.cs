using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Driver, DriverDTO>();
                }
            ).CreateMapper();
    }
}
