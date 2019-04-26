using System;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}
