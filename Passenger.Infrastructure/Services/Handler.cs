﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class Handler : IHandler
    {
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();
        public async Task ExecuteAllAsync()
        {
            foreach (var handlerTask in _handlerTasks)
            {
                await handlerTask.ExecuteASync();
            }
            _handlerTasks.Clear();
        }

        public IHandlerTask Run(Func<Task> run)
        {
            var handlerTask = new HandlerTask(this, run);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTaskRunner Validate(Func<Task> validate)
            => new HandlerTaskRunner(this, validate, _handlerTasks);

        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}