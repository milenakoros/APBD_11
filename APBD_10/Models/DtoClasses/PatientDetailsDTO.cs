namespace APBD_10.Models.DtoClasses;

public class PatientDetailsDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}