using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Passenger.Infrastructure.Commands.Users;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class AccountControllerTest : ControllerTestsBase
    {
        [Fact]
        public async Task Given_valid_current_and_new_password_it_should_be_changed()
        {
            //arrange
            var command = new ChangeUserPassword
            {
                CurrentPassword = "secret",
                NewPassword = "secret2"
            };
            var payload = GetPayload(command);

            //act
            var response = await Client.PutAsync("account/password", payload);

            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
