using Microsoft.EntityFrameworkCore;
using VetApp_BE.Config;
using VetApp_BE.Feature.Appointment.Models;
using VetApp_BE.Feature.Appointment.ViewModels;
using VetApp_BE.GenericRepositories;

namespace VetApp_BE.Feature.Appointment.Repositories
{
    public interface IAppointmentRepository: IRepositoryBase<AppointmentModels>
    {
        public Task<IEnumerable<AppointmentModels>> GetAppointments(AppointmentSearch search, int pageNumber = 1, int pageSize = 10);

    }

    public class AppointmentRepository : RepositoryBase<AppointmentModels>, IAppointmentRepository
    {
        public AppointmentRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppointmentModels>> GetAppointments(AppointmentSearch search, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var query = _context.Appointments.Include(a => a.Pet).AsQueryable();

                if (search.Date.HasValue)
                {
                    query = query.Where(a => a.DateTime.Date == search.Date.Value.Date);
                }

                if (!string.IsNullOrWhiteSpace(search.PetName))
                {
                    query = query.Where(a => a.Pet.Name.Contains(search.PetName));
                }

                query = query.OrderBy(a => a.DateTime);

                return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch(Exception e)
            {
                throw;
            }

        }
    }
}
