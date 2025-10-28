using AW_NHS.API.Models;

namespace AW_NHS.API.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientByIDAsync(int patientID);
    }
}
