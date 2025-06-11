using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Constants.Constants;
using FluentAssertions;
using Xunit;

namespace AcadEvalSys.Application.Tests.User;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.Student)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var user = new CurrentUser("testuser", "Test User", [UserRoles.Admin]);
        
        // Act
        var isInRole = user.IsInRole(UserRoles.Admin);
        
        // Assert
        isInRole.Should().BeTrue();
    }
    
    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var user = new CurrentUser("testuser", "Test User", [UserRoles.Admin]);
        
        // Act
        var isInRole = user.IsInRole(UserRoles.Student);
        
        // Assert
        isInRole.Should().BeFalse();
    }
    
    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var user = new CurrentUser("testuser", "Test User", [UserRoles.Admin]);
        
        // Act
        var isInRole = user.IsInRole(UserRoles.Student.ToLower());
        
        // Assert
        isInRole.Should().BeFalse();
    }
}