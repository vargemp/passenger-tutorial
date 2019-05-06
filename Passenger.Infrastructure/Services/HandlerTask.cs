using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Services
{
    public class HandlerTask : IHandlerTask
    {
        private readonly IHandler _handler;
        private readonly Func<Task> _run;
        private Func<Task> _validate;
        private Func<Task> _always;
        private Func<Task> _onSuccess;
        private Func<Exception, Task> _onError;
        private Func<PassengerException, Task> _onCustomError;

        private bool _propagateException = true;
        private bool _executeOnError = true;

        public HandlerTask(IHandler handler, Func<Task> run, Func<Task> validate = null)
        {
            _handler = handler;
            _run = run;
            _validate = validate;
        }
        public IHandlerTask Always(Func<Task> always)
        {
            _always = always;
            return this;
        }

        public IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
        {
            _onCustomError = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false, bool executeOnError = false)
        {
            _onError = onError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccess = onSuccess;
            return this;
        }

        public IHandlerTask PropagateException()
        {
            _propagateException = true;
            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;
            return this;
        }

        public IHandler Next() => _handler;

        public async Task ExecuteASync()
        {
            try
            {
                if (_validate != null)
                {
                    await _validate();
                }

                await _run();

                if (_onSuccess != null)
                {
                    await _onSuccess();
                }
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(e);

                if (_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if (_always != null)
                {
                    await _always();
                }
            }
        }

        private async Task HandleExceptionAsync(Exception e)
        {
            var customExc = e as PassengerException;
            if (customExc != null)
            {
                if (_onCustomError != null)
                {
                    await _onCustomError(customExc);
                }
            }

            var executeOnError = _executeOnError || customExc == null;
            if (!executeOnError)
            {
                return;
            }

            if (_onError != null)
            {
                await _onError(e);
            }
        }
    }
}
