using APBD_10.Context;
using APBD_10.Models;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Repositories;

public class Repository : IRepository
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Patient> GetPatientByNameAsync(string firstName, string lastName)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Medicament> GetMedicamentByIdAsync(int id)
    {
        return await _context.Medicaments.FindAsync(id);
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }
    public async Task<Patient> GetPatientByIdAsync(int patientId)
    {
        return await _context.Patients.FindAsync(patientId);
    }

    public async Task<List<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId)
    {
        return await _context.Prescriptions
            .Include(p => p.Prescription_Medicaments)
            .Include(p => p.Doctor)
            .Where(p => p.IdPatient == patientId)
            .ToListAsync();
    }
}