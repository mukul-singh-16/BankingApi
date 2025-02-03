using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BankingApp.Application.DTOs;
using BankingApp.Application.Queries;
using MediatR;
using BankingApp.Core.DTOs;
using BankingApp.Api.Controllers;
using BankingApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class Test
{

    [TestMethod]
    public async Task GetAllUsersAsyncTest()
    {
        // Arrange
        var mockUsers = new List<UserDto>
        {
            new UserDto { Id = Guid.NewGuid(), Username = "Nitin" },
            new UserDto { Id = Guid.NewGuid(), Username = "Mukul" }
        };

        var mockSender = new Mock<ISender>();
        var userService = new UsersController(mockSender.Object);

        mockSender.Setup(s => s.Send(It.IsAny<GetAllUserQuery>(), default))
                .ReturnsAsync(mockUsers);

        // Act
        // List<UserDto> result = await userService.GetAllUsersAsync();
        // Assert.IsNotNull(result);

        // // var users = result.ToList(); // No need for .Value here since the service returns IEnumerable<UserDto>
        // var users = result.Value.ToList();
        // Assert.AreEqual(mockUsers.Count, users.Count);
        // Assert.AreEqual("Nitin", users[0].Username);
        // Assert.AreEqual("Mukul", users[1].Username);
    }


    [TestMethod]
    public async Task GetUserData_ReturnsUserProfileDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockSender = new Mock<ISender>();
        var expectedUserProfile = new UserProfileDto { Id = userId, Username = "John Doe", Email = "john@gmail.com", Balance = 14300 };

        var claims = new List<Claim> { new Claim("Id", userId.ToString()) };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var userService = new UsersController(mockSender.Object)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = claimsPrincipal }
            }
        };

        mockSender.Setup(s => s.Send(It.IsAny<GetUserByIdQuery>(), default))
                  .ReturnsAsync(expectedUserProfile);


        // Act
        var result = await userService.GetUserData();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedUserProfile.Id, result.Id);
        Assert.AreEqual(expectedUserProfile.Username, result.Username);
        Assert.AreEqual(expectedUserProfile.Email, result.Email);
        Assert.AreEqual(expectedUserProfile.Balance, result.Balance);
    }
}
