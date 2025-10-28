using AW_NHS.API.Models;
using AW_NHS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AW_NHS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly IPatientService _patientService;

    public PatientController(ILogger<PatientController> logger, IPatientService patientService)
    {
        _logger = logger;
        _patientService = patientService;
    }

    /// <summary>
    /// Get a patient by id.
    /// </summary>
    /// <param name="patientId">Patient id (int).</param>
    /// <returns>200 + patient, 400 when id invalid, 404 when not found, 500 on error</returns>
    [HttpGet("{patientId:int}")]
    [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Patient>> Get(int patientId)
    {
        if (patientId < 0)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid patient ID",
                Detail = "patientId must be a non-negative integer.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        _logger.LogInformation("Received request for patient with ID: {PatientID}", patientId);

        try
        {
            var patient = await _patientService.GetPatientByIDAsync(patientId);

            if (patient == null)
            {
                _logger.LogInformation("Patient with ID: {PatientID} not found", patientId);
                return NotFound();
            }

            _logger.LogInformation("Returning patient with ID: {PatientID}", patientId);

            return Ok(patient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while retrieving patient {PatientID}", patientId);
            return Problem(
                detail: "An unexpected error occurred while retrieving the patient.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
