namespace APBD_10.Models.DtoClasses;

public class PrescriptionRequest
{
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public PatientDTO Patient { get; set; }
    public int IdDoctor { get; set; }
    public List<PrescriptionMedicamentDTO> Medicaments { get; set; }
}