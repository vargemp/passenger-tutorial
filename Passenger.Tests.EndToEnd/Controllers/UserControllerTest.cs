using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Passenger.Api;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class UserControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UserControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Given_valid_email_user_should_exist()
        {
            //act
            var email = "user1@gmail.com";
            var response = await _client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(responseString);


            //assert
            Assert.Equal(user.Email, email);
        }

        [Fact]
        public async Task Given_invalid_email_user_should_not_exist()
        {
            var email = "user99@gmail.com";
            var response = await _client.GetAsync($"users/{email}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
