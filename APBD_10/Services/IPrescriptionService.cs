using APBD_10.Models;
using APBD_10.Models.DtoClasses;

namespace YourNamespace.Services
{
    public interface IPrescriptionService
    {
        Task AddPrescriptionAsync(PrescriptionRequest request);    }
}