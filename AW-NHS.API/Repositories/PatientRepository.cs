using AW_NHS.API.Data;
using AW_NHS.API.Models;

namespace AW_NHS.API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ILogger<PatientRepository> _logger;
        private readonly IPatientDbContext _context;

        public PatientRepository(ILogger<PatientRepository> logger, IPatientDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<Patient> GetPatientByIDAsync(int patientID)
        {
            _logger.LogDebug("Fetching patient with ID: {PatientID}", patientID);

            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientID);

            return Task.FromResult(patient);
        }
    }
}
