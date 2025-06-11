using AcadEvalSys.Application.Competencies.Commands.CreateCompetency;
using AcadEvalSys.Domain.Enums;
using AcadEvalSys.Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AcadEvalSys.Application.Tests.Competencies.CreateCompetency;

public class CreateCompetencyCommandValidatorTests
{
    [Fact()]
    public async Task Validator_ForValidCommand_ShouldNotHaveValidationsErrors()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        
     
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new CreateCompetencyCommand()
        {
            Name = "Test Competency",
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new CreateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact()]
    public async Task Validator_ForDuplicateName_ShouldHaveValidationError()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        
        // Configure mock to return true (competency already exists)
        mockRepository.Setup(x => x.ExistsByNameAsync("Existing Competency"))
                     .ReturnsAsync(true);
        
        var command = new CreateCompetencyCommand()
        {
            Name = "Existing Competency",
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new CreateCompetencyCommandValidator(mockRepository.Object);
        
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
        
        var command = new CreateCompetencyCommand()
        {
            Name = invalidName,
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new CreateCompetencyCommandValidator(mockRepository.Object);
        
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
        
        var command = new CreateCompetencyCommand()
        {
            Name = new string('A', 101), // 101 characters (exceeds max of 100)
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        var validator = new CreateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact()]
    public async Task Validator_ForTooLongDescription_ShouldHaveValidationError()
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new CreateCompetencyCommand()
        {
            Name = "Valid Competency",
            Description = new string('A', 251), // 251 characters (exceeds max of 250)
            Type = CompetencyType.Soft
        };
        
        var validator = new CreateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Theory]
    [InlineData((CompetencyType)999)] // Invalid enum value
    public async Task Validator_ForInvalidType_ShouldHaveValidationError(CompetencyType invalidType)
    {
        // Arrange
        var mockRepository = new Mock<ICompetencyRepository>();
        mockRepository.Setup(x => x.ExistsByNameAsync(It.IsAny<string>()))
                     .ReturnsAsync(false);
        
        var command = new CreateCompetencyCommand()
        {
            Name = "Valid Competency",
            Description = "Valid description",
            Type = invalidType
        };
        
        var validator = new CreateCompetencyCommandValidator(mockRepository.Object);
        
        // Act & Assert
        var result = await validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }
}