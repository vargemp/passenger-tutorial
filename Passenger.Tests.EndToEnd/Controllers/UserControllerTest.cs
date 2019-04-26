using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Passenger.Api;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class UserControllerTest : ControllerTestsBase
    {
        [Fact]
        public async Task Given_valid_email_user_should_exist()
        {
            //arrange
            var email = "user1@test.com";
            

            //act
            var user = await GetUserAsync(email);

            //assert
            Assert.Equal(user.Email, email);
        }

        [Fact]
        public async Task Given_invalid_email_user_should_not_exist()
        {
            var email = "user99@gmail.com";
            var response = await Client.GetAsync($"users/{email}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Given_uniqe_email_user_should_be_created()
        {
            //arrange
            var request = new
            {
                email = "user10@gmail.com",
                password = "blablabla",
                username = "tomtom"
            };
            var payload = GetPayload(request);

            //act
            var response = await Client.PostAsync("users/", payload);

            //assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal($"users/{request.email}", response.Headers.Location.ToString());

            var user = GetUserAsync(request.email);
            Assert.Equal(user.Result.Email, request.email);
        }

        private async Task<UserDTO> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDTO>(responseString);
        }
    }
}
