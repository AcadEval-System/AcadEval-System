using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEvalSys.Application.Career.Commands.CreateCareer;
using AcadEvalSys.Application.Career.Commands.UpdateCareer;
using FluentValidation.TestHelper;
using Xunit;

namespace AcadEvalSys.API.Tests.Career.Validators.UpdateCareer
{
    public class UpdateCareerCommandValidatorTest
    {
        private readonly UpdateCareerCommandValidator _validator;

        public UpdateCareerCommandValidatorTest()
        {
            _validator = new UpdateCareerCommandValidator();
        }

        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new UpdateCareerCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Career Test"
            };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(invalidName: "")]
        [InlineData(invalidName: null)]
        [InlineData(invalidName: "AB")] //toshort, at least 3 chars.

        public void Validator_ForInvalidName_ShouldHaveValidationError(string invalidName)
        {
            // Arrange
            var command = new UpdateCareerCommand() 
            {
                Id = Guid.NewGuid(),
                Name = invalidName 
            };

            // Act & Assert
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(memberAccessor:x:UpdateCareerCommand => x.Name);
        }

        [Fact]
        public void Validator_ForTooLongName_ShouldHaveValidationError()
        {
            // Arrange
            var command = new UpdateCareerCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Career Test"
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(memberAccessor:x:UpdateCareerCommand => x.Name);
        }
    }
}
