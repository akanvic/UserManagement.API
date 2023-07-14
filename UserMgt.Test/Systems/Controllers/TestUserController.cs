using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Models;
using UserMgt.Core.Entities.Response;
using UserMgt.Repo.Repositories.EntityRepository.Implementation;
using UserMgt.Repo.Repositories.EntityRepository.Interface;
using UserMgt.Repo.Repositories.GenericRepository.Interfaces;
using UserMgt.Service.Implementation;
using UserMgt.Service.Interface;

namespace UserMgt.Test.Systems.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task RegisterUser_ReturnsSuccessResponse()
        {

            // Arrange
            var mockUserRepo = new Mock<IUserRepository>();

            // Arrange
            var mockUserService = new Mock<IUserService>();
            var userManagementService = new UserService(mockUserRepo.Object);
            var newUser = new UserDTO { EmailAddress = "user@example.com", Password = "password"};
            var expectedResponse = new ServiceResponse {
                StatusCode = HttpStatusCode.Created,
                Message = "User created successfully"
            };

            // Mock the RegisterUser method of the UserService
            mockUserService.Setup(u => u.AddUser(newUser))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await userManagementService.AddUser(newUser);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        //[Fact]
        //public async Task UserLogin_ReturnsSuccessResponse()
        //{

        //    Arrange
        //   var mockUserRepo = new Mock<IUserRepository>();
        //    var mockTokenService = new Mock<ITokenService>();
        //    var tokenServiceImple = new TokenService()

        //     Arrange
        //    var authService = new Mock<IAuthService>();
        //    var authServiceImpl = new AuthService(mockUserRepo.Object, mockTokenService.Object);
        //    var newUser = new LoginDTO { EmailAddress = "user@example.com", Password = "password" };


        //    var expectedResponse = new LoginResponse
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Message = "User Login successfully",
        //        Token =
        //    };

        //    Mock the RegisterUser method of the UserService
        //    authService.Setup(u => u.Login(newUser))
        //     .ReturnsAsync(expectedResponse);

        //    Act
        //   var result = await authService.Setups(newUser);

        //    Assert
        //    result.Should().BeEquivalentTo(expectedResponse);
        //}

        //[Fact]
        //public async Task UpdateUser_ReturnsSuccessResponse()
        //{

        //    // Arrange
        //    var mockUserRepo = new Mock<IUserRepository>();

        //    // Arrange
        //    var mockUserService = new Mock<IUserService>();
        //    var userManagementService = new UserService(mockUserRepo.Object);
        //    var newUser = new UpdateUserDTO { UserId = 1,EmailAddress = "user@example.com", Password = "password" };
        //    var expectedResponse = new ServiceResponse
        //    {
        //        StatusCode = HttpStatusCode.NoContent,
        //        Message = "User Updated successfully"
        //    };

        //    // Mock the RegisterUser method of the UserService
        //    mockUserService.Setup(u => u.UpdateUser(newUser))
        //        .ReturnsAsync(expectedResponse);

        //    // Act
        //    var result = await userManagementService.UpdateUser(newUser);

        //    // Assert
        //    result.Should().BeEquivalentTo(expectedResponse);
        //}

    }
}
