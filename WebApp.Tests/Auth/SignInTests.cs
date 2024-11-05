

using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using RikaWebApp.Controllers.Auth;
using RikaWebApp.Models.Auth;
using System.Net;

namespace WebApp.Tests.Auth;

public class SignInTests
{
    private readonly Mock<HttpClient> _mockHttpClient;
    private readonly SignInController _controller;

    public SignInTests()
    {
        _mockHttpClient = new Mock<HttpClient>();
        _controller = new SignInController(_mockHttpClient.Object);
    }

   

    [Fact]
    public async Task SignIn_BadRequest_SetsTempDataError()
    {
        // Arrange
        var model = new SignInModel
        {
            Email = "invalid@example.com",
            Password = "InvalidPassword"
        };

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
        var mockHttpHandler = new Mock<HttpMessageHandler>();

        mockHttpHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponseMessage);

        var client = new HttpClient(mockHttpHandler.Object);
        var controller = new SignInController(client);

        // Act
        var result = await controller.SignIn(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("SignInView", viewResult.ViewName);
        Assert.True(controller.TempData.ContainsKey("ErrorRegister"));
        Assert.Equal("Invalid form data", controller.TempData["ErrorRegister"]);
    }

    [Fact]
    public async Task SignIn_InternalServerError_SetsTempDataError()
    {
        // Arrange
        var model = new SignInModel
        {
            Email = "test@example.com",
            Password = "Password123"
        };

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        var mockHttpHandler = new Mock<HttpMessageHandler>();

        mockHttpHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponseMessage);

        var client = new HttpClient(mockHttpHandler.Object);
        var controller = new SignInController(client);

        // Act
        var result = await controller.SignIn(model);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("SignInView", viewResult.ViewName);
       Assert.True(controller.TempData.ContainsKey("ErrorRegister"));
        Assert.Equal("Internal Server Error", controller.TempData["ErrorRegister"]);
    }
}
