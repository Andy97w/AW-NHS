using AW_NHS.API.Models;

namespace AW_NHS.API.Data
{
    public class MockPatientDbContext : IPatientDbContext
    {
        private readonly List<Patient> _patients;

        public MockPatientDbContext()
        {
            _patients = new List<Patient>
            {
                new Patient { Id = 1, NHSNumber = "1234567891", Name = "John Smith", DateOfBirth = new DateTime(1980, 5, 12), GPPractice = "Tynemouth" },
                new Patient { Id = 2, NHSNumber = "1234567892", Name = "Joe Bloggs", DateOfBirth = new DateTime(1975, 8, 23), GPPractice = "Sunderland" },
                new Patient { Id = 3, NHSNumber = "1234567893", Name = "Jane Doe", DateOfBirth = new DateTime(1990, 2, 14), GPPractice = "Newcastle" },
                new Patient { Id = 4, NHSNumber = "1234567894", Name = "John Doe", DateOfBirth = new DateTime(1965, 11, 30), GPPractice = "Gateshead" },
                new Patient { Id = 5, NHSNumber = "1234567895", Name = "Emma Smith", DateOfBirth = new DateTime(2000, 7, 5), GPPractice = "Sunderland" }
            };
        }

        public IQueryable<Patient> Patients => _patients.AsQueryable();

    }
}
