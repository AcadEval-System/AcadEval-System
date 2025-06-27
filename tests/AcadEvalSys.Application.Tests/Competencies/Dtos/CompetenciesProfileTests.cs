using AcadEvalSys.Application.Competencies.Commands.CreateCompetency;
using AcadEvalSys.Application.Competencies.Commands.UpdateCompetency;
using AcadEvalSys.Application.Competencies.Dtos;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Enums;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace AcadEvalSys.Application.Tests.Competencies.Dtos;

public class CompetenciesProfileTests
{
    [Fact()]
    public void CreateMap_ForCompetencyToCompetencyDto_MapsCorrectly()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CompetencyProfile>()); 

        var mapper = configuration.CreateMapper();
        var competency = new Competency()
        {
            Name = "Test Competency",
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        // Act
        var competencyDto = mapper.Map<CompetencyDto>(competency);
        
        // Assert
        competencyDto.Should().NotBeNull();
        competencyDto.Id.Should().Be(competency.Id);
        competencyDto.Name.Should().Be(competency.Name);
        competencyDto.Description.Should().Be(competency.Description);
        competencyDto.Type.Should().Be(competency.Type);
    }
    
    
    [Fact()]
    public void CreateMap_ForCreateCompetencyCommandToCompetency_MapsCorrectly()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CompetencyProfile>()); 

        var mapper = configuration.CreateMapper();
        var createCompetencyCommand = new CreateCompetencyCommand()
        {
            Name = "Test Competency",
            Description = "This is a test competency.",
            Type = CompetencyType.Soft
        };
        
        // Act
        var competency = mapper.Map<Competency>(createCompetencyCommand);
        
        // Assert
        competency.Should().NotBeNull();
        competency.Id.Should().NotBe(Guid.Empty);
        competency.Name.Should().Be(createCompetencyCommand.Name);
        competency.Description.Should().Be(createCompetencyCommand.Description);
        competency.Type.Should().Be(createCompetencyCommand.Type);
    }

    [Fact()]
    public void CreateMap_ForUpdateCompetencyCommandToCompetency_MapsCorrectly()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CompetencyProfile>()); 

        var mapper = configuration.CreateMapper();
        
        var existingCompetency = new Competency()
        {
            Id = Guid.NewGuid(),
            Name = "Original Competency",
            Description = "Original description",
            Type = CompetencyType.Technical,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            CreatedByUserId = "original-user",
            IsActive = true
        };
        
        var updateCompetencyCommand = new UpdateCompetencyCommand()
        {
            Id = Guid.NewGuid(), // This should be ignored
            Name = "Updated Competency",
            Description = "Updated description",
            Type = CompetencyType.Soft
        };
        
        // Act
        mapper.Map(updateCompetencyCommand, existingCompetency);
        
        // Assert
        existingCompetency.Should().NotBeNull();
        existingCompetency.Id.Should().NotBe(Guid.Empty);
        existingCompetency.Name.Should().Be(updateCompetencyCommand.Name);
        existingCompetency.Description.Should().Be(updateCompetencyCommand.Description);
        existingCompetency.Type.Should().Be(updateCompetencyCommand.Type);
    }
}