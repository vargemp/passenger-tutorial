using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Services;
using Xunit;

namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Register_async_should_invoke_add_async_on_repo()
        {
            //Arrange
            var userRepoMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepoMock.Object, encrypterMock.Object , mapperMock.Object);

            //Act
            await userService.RegisterAsync(Guid.NewGuid(),"tom@mgail.com", "tomtom", "abrakadabra", "admin");

            //Assert
            userRepoMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task When_calling_get_async_and_user_exists_should_invoke_user_repository_get_async()
        {
            //Arrange
            var userRepoMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepoMock.Object, encrypterMock.Object, mapperMock.Object);

            //act
            await userService.GetAsync("user1@gmail.com");
            var user = new User(Guid.NewGuid(), "user1@gmail.com", "user1", "secret", "salt", "user");

            //assert
            userRepoMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(user);
            userRepoMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task Register_existing_user_should_throw_exception()
        {
            //Arrange
            var userRepoMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepoMock.Object, encrypterMock.Object ,mapperMock.Object);

            //Act
            //await userService.RegisterAsync("user1@gmail.com", "tomtom", "abrakadabra");
            var ex = await Record.ExceptionAsync(() => userService.RegisterAsync(Guid.NewGuid(), "user1@gmail.com", "tomtom", "abrakadabra", "admin"));

            //Assert
            Assert.IsType<Exception>(ex);
        }
    }
}
