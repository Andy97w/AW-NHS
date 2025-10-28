using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using AW_NHS.API.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AW_NHS.Tests.Integration
{
    public class PatientControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PatientControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetPatient_ReturnsOk_WithValidId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/patient/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var patient = await response.Content.ReadFromJsonAsync<Patient>();
            Assert.NotNull(patient);
            Assert.Equal(1, patient.Id);
            Assert.Equal("John Smith", patient.Name);
        }

        [Fact]
        public async Task GetPatient_ReturnsNotFound_WithInvalidId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/patient/999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPatient_ReturnsBadRequest_WithNegativeId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/patient/-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var problemDetails = await response.Content.ReadFromJsonAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>();
            Assert.Equal(400, problemDetails.Status);
            Assert.Equal("Invalid patient ID", problemDetails.Title);
        }

        [Fact]
        public async Task GetPatient_ReturnsNotFound_WhenIdNotProvided()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/patient");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}