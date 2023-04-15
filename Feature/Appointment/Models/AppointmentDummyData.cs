using Bogus;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using VetApp_BE.Feature.Pets.Models;

namespace VetApp_BE.Feature.Appointment.Models
{
    public static class AppointmentDummyData
    {
        public static List<AppointmentModels> GenerateAppointments(int count)
        {
            var appointmentFaker = new Faker<AppointmentModels>()
                .RuleFor(a => a.Id, f => null)
                .RuleFor(a => a.DateTime, f => f.Date.Future())
                .RuleFor(a => a.Pet, f => new PetModels
                {
                    Id = null,
                    Name = f.Name.FirstName(),
                    OwnerName = f.Name.FullName(),
                    PreferredContactDetails = f.Phone.PhoneNumber()
                });

            return appointmentFaker.Generate(count);
        }
    }
}
