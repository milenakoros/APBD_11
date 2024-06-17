using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;

namespace APBD_10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var patientDetails = await _patientService.GetPatientDetailsAsync(id);
            return Ok(patientDetails);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
