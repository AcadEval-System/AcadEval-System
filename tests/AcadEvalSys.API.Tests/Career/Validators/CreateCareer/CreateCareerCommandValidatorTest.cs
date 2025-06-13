using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEvalSys.Application.Career.Commands.CreateCareer;
using Xunit;
using FluentValidation.TestHelper;

namespace AcadEvalSys.API.Tests.Career.Validators.CreateCareer;

public class CreateCareerCommandValidatorTest() 
{
    private readonly CreateCareerCommandValidator _validator;
    public CreateCareerCommandValidatorTest()
    {
        _validator = new CreateCareerCommandValidator();
    }

    [Fact]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CreateCareerCommand()
        {
            Name = "Career Test"
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(invalidName:"")]
    [InlineData(invalidName: null)]
    [InlineData(invalidName: "AB")] //toshort, at least 3 chars.

    public void Validator_ForInvalidName_ShouldHaveValidationError(string invalidName)
    {
        // Arrange
        var command = new CreateCareerCommand() { Name = invalidName };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(memberAccessor:x:CreateCareerCommand => x.Name);
    }

    [Fact]
    public void Validator_ForTooLongName_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCareerCommand()
        {
            Name = new string(c:'A', count:101)
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(memberAccessor:x:CreateCareerCommand => x.Name);
    }



}
