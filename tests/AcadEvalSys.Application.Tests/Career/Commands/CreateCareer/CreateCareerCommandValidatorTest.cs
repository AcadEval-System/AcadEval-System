using AcadEvalSys.Application.Career.Commands.CreateCareer;
using FluentValidation.TestHelper;

namespace AcadEvalSys.Application.Tests.Career.Commands.CreateCareer;

public class CreateCareerCommandValidatorTests
{
    private readonly CreateCareerCommandValidator _validator;

    public CreateCareerCommandValidatorTests()
    {
        _validator = new CreateCareerCommandValidator();
    }

    [Fact]
    public void Validator_ForValidCommnad_ShouldNotHaveValidationErrors()
    {
        var command = new CreateCareerCommand()
        {
            Name = "Test Career"
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void Validator_ForInvalidName_ShouldHaveValidationErrors()
    {
        var command = new CreateCareerCommand()
        {
            Name = string.Empty // Invalid name
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    [Fact]
    public void Validator_ForTooLongName_ShouldHaveValidationErrors()
    {
        var command = new CreateCareerCommand()
        {
            Name = new string('a', 200)
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    [Fact]
    public void Validator_ForNullName_ShouldHaveValidationErrors()
    {
        var command = new CreateCareerCommand()
        {
            Name = null // Invalid name
        };
        
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }
    
    
}