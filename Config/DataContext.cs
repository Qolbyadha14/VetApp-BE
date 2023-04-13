using Microsoft.EntityFrameworkCore;
using VetApp_BE.Feature.Appointment.Models;
using VetApp_BE.Feature.Pets.Models;

namespace VetApp_BE.Config
{
    public class DataContext : DbContext
    {
        public DbSet<AppointmentModels> Appointments { get; set; }
        public DbSet<PetModels> Pets { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

    }
}
