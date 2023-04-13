using VetApp_BE.Feature.Pets.ViewModels;

namespace VetApp_BE.Feature.Appointment.ViewModels
{
    public class AppointmentViewModels
    {
        public DateTime DateTime { get; set; }
        public PetViewModels Pet { get; set; }
    }
}
