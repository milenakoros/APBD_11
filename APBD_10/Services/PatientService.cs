using APBD_10.Models.DtoClasses;
using YourNamespace.Repositories;

namespace YourNamespace.Services;

public class PatientService : IPatientService
{
    private readonly IRepository _repository;

    public PatientService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId)
    {
        var patient = await _repository.GetPatientByIdAsync(patientId);
        if (patient == null)
        {
            throw new Exception("Patient not found");
        }

        var prescriptions = await _repository.GetPrescriptionsByPatientIdAsync(patientId);

        var patientDetails = new PatientDetailsDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionDTO
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Medicaments = p.Prescription_Medicaments.Select(pm => new PrescriptionMedicamentDTO
                {
                    IdMedicament = pm.IdMedicament,
                    Dose = pm.Dose,
                    Details = pm.Details
                }).ToList(),
                Doctor = new DoctorDTO
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName,
                    Email = p.Doctor.Email
                }
            }).ToList()
        };

        return patientDetails;
    }
}