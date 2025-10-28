using AW_NHS.API.Models;

namespace AW_NHS.API.Services
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByIDAsync(int patientID);
    }
}
