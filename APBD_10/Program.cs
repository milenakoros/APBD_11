using APBD_10.Context;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Repositories;
using YourNamespace.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        // Registering ApplicationDbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register the PrescriptionService
        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
        builder.Services.AddScoped<IPatientService, PatientService>();
        builder.Services.AddScoped<IRepository, Repository>();


        var app = builder.Build();

        // Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization(); // Make sure to include authorization middleware if needed
        app.MapControllers();

        app.Run();
    }
}