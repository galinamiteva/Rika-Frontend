﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using RikaWebApp.Controllers;
using RikaWebApp.Models;
using RikaWebApp.Models.Auth;
using System.Net;
using System.Text;

namespace WebApp.Tests.Auth;
// Gala tests!!
public class SignUpTests 
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _mockHttpClient;
    private readonly SignUpController _controller;

    public SignUpTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);

        // Mock TempData and set it in the controller
        var tempData = new Mock<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary>();
        _controller = new SignUpController(_mockHttpClient) { TempData = tempData.Object };
    }

    // Helper method to set up HTTP response
    private void SetupHttpResponse(HttpStatusCode statusCode, string content = "")
    {
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            });
    }

    [Fact]
    public async Task SignUp_ReturnsConflict_WhenEmailAlreadyExists()
    {
        // Arrange
        var model = new SignUpModel { Email = "test@example.com" };
        SetupHttpResponse(HttpStatusCode.Conflict);  

        // Act
        var result = await _controller.SignUp(model) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SignUpView", result.ViewName); 
        
    }

    [Fact]
    public async Task SignUp_ReturnsBadRequest_WhenInvalidData()
    {
        // Arrange
        var model = new SignUpModel { Email = "" }; 
        SetupHttpResponse(HttpStatusCode.BadRequest); 

        // Act
        var result = await _controller.SignUp(model) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SignUpView", result.ViewName); 
    }

    [Fact]
    public async Task SignUp_ReturnsInternalServerError_WhenServerFails()
    {
        // Arrange
        var model = new SignUpModel { Email = "test@example.com" };
        SetupHttpResponse(HttpStatusCode.InternalServerError); 

        // Act
        var result = await _controller.SignUp(model) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SignUpView", result.ViewName); 
        
    }

   
}
