using AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;
using AcadEvalSys.Domain.Enums;
using AcadEvalSys.Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AcadEvalSys.Application.Tests.Competencies.UpdateCompetency;

public class UpdateCompetencyCommandValidatorTests
{
    [Fact()]
    public async Task Validator_ForValidCommand_ShouldNotHaveValidationsErrors()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = "Updated Competency",
            Description = "This is an updated competency.",
            Type = CompetencyType.Soft
        };

        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact()]
    public async Task Validator_ForDuplicateName_ShouldHaveValidationError()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        
        mockRepository.Setup(x => x.ExistsByNameAsync("Existing Competency"))
            .ReturnsAsync(true);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = "Existing Competency",
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("A competency with this name already exists.");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("AB")]
    public async Task Validator_ForInvalidName_ShouldHaveValidationError(string invalidName)
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = invalidName,
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact()]
    public async Task Validator_ForTooLongName_ShouldHaveValidationError()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = new string('A', 101), // 101 characters (exceeds max of 100)
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validator_ForInvalidDescription_ShouldHaveValidationError(string invalidDescription)
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = "Valid Competency",
            Description = invalidDescription,
            Type = CompetencyType.Soft
        };
        
        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorMessage("Competency description is required.");
    }
    
    [Fact()]
    public async Task Validator_ForTooLongDescription_ShouldHaveValidationError()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = "Valid Competency",
            Description = new string('A', 501), // 501 characters (exceeds max of 500)
            Type = CompetencyType.Soft
        };
        
        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorMessage("Competency description must not exceed 500 characters.");
    }
    
    [Theory]
    [InlineData((CompetencyType)999)] // Invalid enum value
    public async Task Validator_ForInvalidType_ShouldHaveValidationError(CompetencyType invalidType)
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(),
            Name = "Valid Competency",
            Description = "Valid description",
            Type = invalidType
        };
        
        var validator = new UpdateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Type)
              .WithErrorMessage("Invalid competency type.");
    }
}