using System;
using System.Threading.Tasks;
using AW_NHS.API.Models;
using AW_NHS.API.Repositories;
using AW_NHS.API.Data;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW_NHS.API.Tests.Repositories
{
    public class PatientRepositoryTests
    {
        [Fact]
        public async Task GetPatientByIDAsync_ReturnsPatient_WhenPatientExists()
        {
            // Arrange: Set up logger, context, and repository
            var mockLogger = new Mock<ILogger<PatientRepository>>();
            var context = new MockPatientDbContext();
            var repository = new PatientRepository(mockLogger.Object, context);

            // Act: Call the repository method with an existing patient ID
            var patient = await repository.GetPatientByIDAsync(1);

            // Assert: Check that the patient is returned and has expected properties
            Assert.NotNull(patient);
            Assert.Equal(1, patient.Id);
            Assert.Equal("John Smith", patient.Name);
        }

        [Fact]
        public async Task GetPatientByIDAsync_ReturnsNull_WhenPatientDoesNotExist()
        {
            // Arrange: Set up logger, context, and repository
            var mockLogger = new Mock<ILogger<PatientRepository>>();
            var context = new MockPatientDbContext();
            var repository = new PatientRepository(mockLogger.Object, context);

            // Act: Call the repository method with a non-existent patient ID
            var patient = await repository.GetPatientByIDAsync(999);

            // Assert: Check that null is returned
            Assert.Null(patient);
        }
    }
}