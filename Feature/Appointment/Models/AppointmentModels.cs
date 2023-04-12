using VetApp_BE.Feature.Pets.Models;

namespace VetApp_BE.Feature.Appointment.Models
{
    public class AppointmentModels
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public PetModels Pet { get; set; }
    }
}
