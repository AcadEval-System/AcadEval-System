using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using FluentAssertions;

namespace AcadEvalSys.API.Tests.Controllers
{
    public class CareerControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public CareerControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_ForValidRequest_Return200Ok()
        {
            var client = _factory.CreateClient();

            var result = await client.GetAsync("/careers?pageNumber=1&pageSize=10");

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            
        }
    }
}
