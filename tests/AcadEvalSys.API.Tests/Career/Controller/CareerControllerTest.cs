using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using Moq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AcadEvalSys.API.Tests;
using AcadEvalSys.Application.Career.Commands.CreateCareer;
using AcadEvalSys.Application.Career.Commands.DeleteCareer;
using AcadEvalSys.Application.Career.Commands.UpdateCareer;
using AcadEvalSys.Application.Career.Dtos;
using AcadEvalSys.Application.Users;
using AcadEvalSys.Domain.Constants.Constants;
using AcadEvalSys.Domain.Entities;
using AcadEvalSys.Domain.Repositories;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AcadEvalSys.API.tests.Career.Controller
{
    public class CareerControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ICareerRepository> _careerRepositoryMock = new();
        private readonly Mock<IUserContext> _userContextMock = new();
        private JsonSerializerOptions _jsonOptions;
        public CareerControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.Replace(ServiceDescriptor.Scoped(typeof(ICareerRepository),
                            _ => _careerRepositoryMock.Object));
                        services.Replace(ServiceDescriptor.Scoped(typeof(IUserContext),
                            _ => _userContextMock.Object));
                    });
                    var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin]);
                    _userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

                    _jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        Converters = { new JsonStringEnumConverter() }
                    };
                }

            );
        }   
        
        [Fact]
        public async Task GetCareers_ForValidRequest_Returns200Ok()
        {
            var careers = new List<TechnicalCareer>
            {
                new() { Id = Guid.NewGuid(), Name = "Career 1" },
                new() { Id = Guid.NewGuid(), Name = "Career 2" }
            };
            _careerRepositoryMock.Setup(repo => repo.GetAllCareersAsync())
                .ReturnsAsync(careers);
            
            var client = _factory.CreateClient();

            var result = await client.GetAsync("/careers");

            result.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task GetCareerById_ForValidId_Returns200Ok()
        {
            var careerId = Guid.NewGuid();
            var career = new TechnicalCareer { Id = careerId, Name = "Career 1" };
            _careerRepositoryMock.Setup(repo => repo.GetCareerByIdAsync(careerId))
                .ReturnsAsync(career);
            
            var client = _factory.CreateClient();

            var result = await client.GetAsync($"/careers/{careerId}");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await result.Content.ReadAsStringAsync();
            var careerDto = JsonSerializer.Deserialize<CareerDto>(content, _jsonOptions);
            careerDto.Should().NotBeNull();
            careerDto.Id.Should().Be(careerId);
        }
        
        [Fact]
        public async Task CreateCareer_ForValidRequest_Returns201Created()
        {
            var newCareer = new CreateCareerCommand { Name = "New Career" };
            var createdId = Guid.NewGuid();
            _careerRepositoryMock.Setup(repo => repo.Create(It.IsAny<TechnicalCareer>()))
                .ReturnsAsync(createdId);
            
            var client = _factory.CreateClient();

            var content = new StringContent(JsonSerializer.Serialize(newCareer, _jsonOptions), 
                System.Text.Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/careers", content);

            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Headers.Location.Should().NotBeNull();
            result.Headers.Location!.AbsolutePath.Should().Be($"/careers/{createdId}");
        }

        [Fact]
        public async Task UpdateCareer_ForValidRequest_Returns204NoContent()
        {
            var careerId = Guid.NewGuid();
            var existingCareer = new TechnicalCareer { Id = careerId, Name = "Existing Career" };
            var updateCareer = new UpdateCareerCommand
            {
                Id = careerId,
                Name = "Updated Career"
            };
            _careerRepositoryMock.Setup(repo => repo.GetCareerByIdAsync(careerId))
                .ReturnsAsync(existingCareer);
            _careerRepositoryMock.Setup(repo => repo.Update())
                .Returns(Task.CompletedTask);
            
            var client = _factory.CreateClient();
            var json = JsonSerializer.Serialize(updateCareer);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var result = await client.PatchAsync($"/careers/{careerId}", content);
            
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task DeleteCareer_ForValidId_Returns204NoContent()
        {
            var careerId = Guid.NewGuid();
            var existingCareer = new TechnicalCareer { Id = careerId, Name = "Career to Delete" };
            _careerRepositoryMock.Setup(repo => repo.GetCareerByIdAsync(careerId))
                .ReturnsAsync(existingCareer);
            _careerRepositoryMock.Setup(repo => repo.Delete(existingCareer))
                .Returns(Task.CompletedTask);
            
            var client = _factory.CreateClient();
            var result = await client.DeleteAsync($"/careers/{careerId}");
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
