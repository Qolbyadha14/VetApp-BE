using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using VetApp_BE.Feature.Pets.Models;

namespace VetApp_BE.Feature.Appointment.Models
{
    [Table("Appointments")]
    public class AppointmentModels
    {
        public int? Id { get; set; }
        public DateTime DateTime { get; set; }
        public PetModels Pet { get; set; }
    }
}
