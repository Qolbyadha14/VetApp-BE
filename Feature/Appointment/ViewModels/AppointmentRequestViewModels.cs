using VetApp_BE.Feature.Pets.ViewModels;

namespace VetApp_BE.Feature.Appointment.ViewModels
{
    public class AppointmentRequestViewModels
    {
        public DateTime DateTime { get; set; }
        public bool is_new_pats { get; set; }
        public PetViewModels Pet { get; set; }
    }
}
