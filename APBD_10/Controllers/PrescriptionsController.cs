using APBD_10.Models.DtoClasses;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(PrescriptionRequest request)
    {
        try
        {
            await _prescriptionService.AddPrescriptionAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}