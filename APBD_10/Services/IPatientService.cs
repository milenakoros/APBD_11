using APBD_10.Models.DtoClasses;

namespace YourNamespace.Services;

public interface IPatientService
{
    Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId);
}

