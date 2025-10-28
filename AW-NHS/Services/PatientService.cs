using AW_NHS.API.Models;
using AW_NHS.API.Repositories;

namespace AW_NHS.API.Services
{
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;
        private readonly IPatientRepository _patientRepository;

        public PatientService(ILogger<PatientService> logger, IPatientRepository patientRepository)
        {
            _logger = logger;
            _patientRepository = patientRepository;
        }


        public async Task<Patient> GetPatientByIDAsync(int patientID)
        {
            _logger.LogDebug("Fetching patient with ID: {PatientID}", patientID);

            var patient = await _patientRepository.GetPatientByIDAsync(patientID);

            return patient;
        }
    }
}
