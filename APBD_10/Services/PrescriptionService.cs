using APBD_10.Models;
using APBD_10.Models.DtoClasses;
using YourNamespace.Repositories;
using YourNamespace.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IRepository _repository;

    public PrescriptionService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task AddPrescriptionAsync(PrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
        {
            throw new Exception("Prescription can include a maximum of 10 medicaments.");
        }

        if (request.DueDate < request.Date)
        {
            throw new Exception("DueDate must be greater than or equal to Date.");
        }

        var patient = await _repository.GetPatientByNameAsync(request.Patient.FirstName, request.Patient.LastName);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            await _repository.AddPatientAsync(patient);
        }

        foreach (var med in request.Medicaments)
        {
            var medicament = await _repository.GetMedicamentByIdAsync(med.IdMedicament);
            if (medicament == null)
            {
                throw new Exception($"Medicament with Id {med.IdMedicament} does not exist.");
            }
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = request.IdDoctor,
            IdPatient = patient.IdPatient,
            Prescription_Medicaments = request.Medicaments.Select(m => new Prescription_Medicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        await _repository.AddPrescriptionAsync(prescription);
    }
}