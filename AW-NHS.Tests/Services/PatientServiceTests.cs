using AW_NHS.API.Models;
using AW_NHS.API.Services;
using AW_NHS.API.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace AW_NHS.API.Tests.Services
{
    public class PatientServiceTests
    {
        [Fact]
        public async Task GetPatientByIDAsync_ReturnsPatient_WhenPatientExists()
        {
            // Arrange: Set up logger, repository, and expected patient
            var mockLogger = new Mock<ILogger<PatientService>>();
            var mockRepository = new Mock<IPatientRepository>();
            var expectedPatient = new Patient { Id = 1, NHSNumber = "1234567891", Name = "John Smith", DateOfBirth = new DateTime(1980, 5, 12), GPPractice = "Tynemouth" };
            mockRepository.Setup(r => r.GetPatientByIDAsync(1)).ReturnsAsync(expectedPatient);
            var service = new PatientService(mockLogger.Object, mockRepository.Object);

            // Act: Call the service method
            var patient = await service.GetPatientByIDAsync(1);

            // Assert: Check that the patient is returned and has expected properties
            Assert.NotNull(patient);
            Assert.Equal(expectedPatient.Id, patient.Id);
            Assert.Equal(expectedPatient.Name, patient.Name);
        }

        [Fact]
        public async Task GetPatientByIDAsync_ReturnsNull_WhenPatientDoesNotExist()
        {
            // Arrange: Set up logger and repository to return null
            var mockLogger = new Mock<ILogger<PatientService>>();
            var mockRepository = new Mock<IPatientRepository>();
            mockRepository.Setup(r => r.GetPatientByIDAsync(999)).ReturnsAsync((Patient?)null);
            var service = new PatientService(mockLogger.Object, mockRepository.Object);

            // Act: Call the service method with a non-existent patient ID
            var patient = await service.GetPatientByIDAsync(999);

            // Assert: Check that null is returned
            Assert.Null(patient);
        }
    }
}
