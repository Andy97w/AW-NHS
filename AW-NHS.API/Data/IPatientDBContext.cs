using AW_NHS.API.Models;

namespace AW_NHS.API.Data
{
    public interface IPatientDbContext
    {
        IQueryable<Patient> Patients { get; }
    }
}
