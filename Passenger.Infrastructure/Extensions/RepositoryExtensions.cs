using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Driver> GetOrFailAsync(this IDriverRepository driverRepository, Guid userId)
        {
            var driver = await driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user id {userId} wasnt found.");
            }

            return driver;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid userId)
        {
            var user = await userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with user id {userId} wasnt found.");
            }

            return user;
        }
    }
}
