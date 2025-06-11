using System.Security.Claims;
using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Constants.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace AcadEvalSys.Application.Tests.User;

public class UserContextTest
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // Arrange
        var httpContextAccessMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, "1"),
            new (ClaimTypes.Email, "admin@admin.com"),
            new (ClaimTypes.Role, UserRoles.Admin),
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });
        var userContext = new UserContext(httpContextAccessMock.Object);

        // Act

        var currentUser = userContext.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser!.Id.Should().Be("1");
        currentUser.Email.Should().Be("admin@admin.com");
        currentUser.Roles.Should().Contain(UserRoles.Admin);
    }
    
    [Fact()]
    public void GetCurrentUser_WithUnauthenticatedUser_ShouldReturnNull()
    {
        // Arrange
        var httpContextAccessMock = new Mock<IHttpContextAccessor>();
        httpContextAccessMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
        var userContext = new UserContext(httpContextAccessMock.Object);

        // Act
       Action action = () => userContext.GetCurrentUser();

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User context not found");
    }
}