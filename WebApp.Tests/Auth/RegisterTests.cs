﻿//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Moq.Protected;
//using RikaWebApp.Controllers;
//using RikaWebApp.Models;
//using RikaWebApp.Models.Auth;
//using System.Net;
//using System.Text;

//namespace WebApp.Tests.Auth;
//// Mara Tests - fins nåt fel
//public class RegisterTests
//{
//    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
//    private readonly HttpClient _mockHttpClient;
//    private readonly SignUpController _controller;
//    private readonly Mock<SignUpController> _controllerMock;

//    public RegisterTests()
//    {
//        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
//        _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);

//        // Mock TempData och sätt in det i kontrollern
//        var tempData = new Mock<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary>();
//        _controller = new SignUpController(_mockHttpClient) { TempData = tempData.Object };

//        var _controllerMock = new Mock<SignUpController>();
//    }

//    private void SetupHttpResponse(HttpStatusCode statusCode, string content = "")
//    {
//        _mockHttpMessageHandler.Protected()
//            .Setup<Task<HttpResponseMessage>>(
//                "SendAsync",
//                ItExpr.IsAny<HttpRequestMessage>(),
//                ItExpr.IsAny<CancellationToken>()
//            )
//            .ReturnsAsync(new HttpResponseMessage
//            {
//                StatusCode = statusCode,
//                Content = new StringContent(content, Encoding.UTF8, "application/json")
//            });
//    }

//    [Fact]
//    public async Task Register_ReturnsConflict_WhenEmailAlreadyExists()
//    {
//        // Arrange
//        var model = new SignUpModel { Email = "test@example.com" };
//        var statusCode = System.Net.HttpStatusCode.Conflict;

//        // Act
//        var result = await _controller.SignUp(model) as ViewResult;

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("SignUpView", result.ViewName);
//    }

//    [Fact]
//    public async Task Register_ReturnsBadRequest_WhenInvalidData()
//    {
//        // Arrange
//        var model = new SignUpModel { Email = "" }; // Ogiltig modell (tomma fält)
//        SetupHttpResponse(HttpStatusCode.BadRequest);

//        // Act
//        var result = await _controller.SignIn(model) as ViewResult;

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("SignUpView", result.ViewName);
//    }

//    [Fact]
//    public async Task Register_ReturnsInternalServerError()
//    {
//        // Arrange
//        var model = new SignUpModel { Email = "test@example.com" };
//        SetupHttpResponse(HttpStatusCode.InternalServerError);

//        // Act
//        var result = await _controller.SignIn(model) as ViewResult;

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("SignUpView", result.ViewName);
//    }
//}
