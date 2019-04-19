using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
