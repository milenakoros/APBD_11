using System.ComponentModel.DataAnnotations;
using APBD_10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=146.19.215.204,55555;Database=milen;User Id=sa;Password=Nienawidzepj@tk!;TrustServerCertificate=true;")
            .LogTo(Console.WriteLine,LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new
            {
                pm.IdMedicament, pm.IdPrescription
            });

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdPrescription);
    }
}