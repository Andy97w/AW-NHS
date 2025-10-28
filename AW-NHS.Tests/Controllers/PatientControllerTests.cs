using Xunit;
using Moq;
using AW_NHS.API.Models;
using AW_NHS.API.Services;
using AW_NHS.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AW_NHS.Tests.Controllers
{
    public class PatientControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WithPatient()
        {
            // Arrange: Set up logger, service, and expected patient
            var mockLogger = new Mock<ILogger<PatientController>>();
            var mockService = new Mock<IPatientService>();
            var patient = new Patient { Id = 1, NHSNumber = "1234567891", Name = "John Smith", DateOfBirth = new DateTime(1980, 5, 12), GPPractice = "Tynemouth" };
            mockService.Setup(s => s.GetPatientByIDAsync(1)).ReturnsAsync(patient);
            var controller = new PatientController(mockLogger.Object, mockService.Object);

            // Act: Call the controller method
            var result = await controller.Get(1);

            // Assert: Check for OkObjectResult and correct patient returned
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(patient, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange: Set up logger and service to return null for patient
            var mockLogger = new Mock<ILogger<PatientController>>();
            var mockService = new Mock<IPatientService>();
            mockService.Setup(s => s.GetPatientByIDAsync(999)).ReturnsAsync((Patient?)null);
            var controller = new PatientController(mockLogger.Object, mockService.Object);

            // Act: Call the controller method with non-existent patient ID
            var result = await controller.Get(999);

            // Assert: Check for NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange: Set up logger and service to throw exception
                var mockLogger = new Mock<ILogger<PatientController>>();
            var mockService = new Mock<IPatientService>();
            mockService.Setup(s => s.GetPatientByIDAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));
            var controller = new PatientController(mockLogger.Object, mockService.Object);

            // Act: Call the controller method
            var result = await controller.Get(2);

            // Assert: Check for ObjectResult with status code 500
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task Get_ReturnsBadRequest_WhenPatientIdIsNegative()
        {
            // Arrange: Set up logger and service
            var mockLogger = new Mock<ILogger<PatientController>>();
            var mockService = new Mock<IPatientService>();
            var controller = new PatientController(mockLogger.Object, mockService.Object);

            // Act: Call the controller method with negative patient ID
            var result = await controller.Get(-1);

            // Assert: Check for BadRequestObjectResult and ProblemDetails
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var problemDetails = Assert.IsType<ProblemDetails>(badRequestResult.Value);
            Assert.Equal(400, problemDetails.Status);
            Assert.Equal("Invalid patient ID", problemDetails.Title);
        }
    }
}