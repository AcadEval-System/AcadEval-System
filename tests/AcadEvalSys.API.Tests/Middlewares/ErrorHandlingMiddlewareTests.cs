using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Exceptions;
using AcadEvalSys.WEB.Server.Middlewares;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common.Exceptions;
using Moq;
using Xunit;

namespace AcadEvalSys.API.Tests.Middlewares;

public class ErrorHandlingMiddlewareTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
    private readonly ErrorHandlingMiddleware _middleware;
    private readonly IHostEnvironment _hostEnvironment;
    
    public ErrorHandlingMiddlewareTests()
    {
        _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var envMock = new Mock<IHostEnvironment>();
        envMock.SetupGet(e => e.EnvironmentName).Returns("Development");
        _hostEnvironment = envMock.Object;
        _middleware = new ErrorHandlingMiddleware(_loggerMock.Object, _hostEnvironment);
    }
    
    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
       //arange
       var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
       var middleware = new ErrorHandlingMiddleware(loggerMock.Object, _hostEnvironment);
       var context = new DefaultHttpContext();
       var nextDelegate = new Mock<RequestDelegate>();
         //act
       await middleware.InvokeAsync(context, nextDelegate.Object);
       
       
       //assert
       nextDelegate.Verify(next => next(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldReturn404()
    {
        var context = new DefaultHttpContext();
        var exception = new NotFoundException(nameof(Competency), "Competency");
    
        // act
        await _middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task InvokeAsync_WhenUnauthorizedExceptionThrown_ShouldReturn401()
    {
        var context = new DefaultHttpContext();
        var exception = new UnauthorizedException();
        // act
        await _middleware.InvokeAsync(context, _ => throw exception);
        context.Response.StatusCode.Should().Be(401);
    }

    [Fact]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldReturn403()
    {
        var context = new DefaultHttpContext();
        var exception = new ForbidException();
        // act
        await _middleware.InvokeAsync(context, _ => throw exception);
        context.Response.StatusCode.Should().Be(403);
    }

    [Fact]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldReturn500()
    {
        var context = new DefaultHttpContext();
        var exception = new Exception("Some error");
        // act
        await _middleware.InvokeAsync(context, _ => throw exception);
        context.Response.StatusCode.Should().Be(500);
    }

    
}