﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Extensions;
using ErrorCodes = Passenger.Core.Domain.ErrorCodes;


namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleProvider _vehicleProvider;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IMapper mapper,
                IUserRepository userRepository, IVehicleProvider vehicleProvider)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
            _mapper = mapper;
        }

        public async Task<DriverDetailsDTO> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<DriverDetailsDTO>(driver);
        }

        public async Task<IEnumerable<DriverDTO>> BrowseAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DriverDTO>>(drivers);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new ServiceException(Passenger.Infrastructure.Exceptions.ErrorCodes.DriverAlreadyExists,$"Driver with id {userId} already exists.");
            }

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            var vehicleDetails = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(brand, name, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            await _driverRepository.DeleteAsync(driver);
        }
    }
}
