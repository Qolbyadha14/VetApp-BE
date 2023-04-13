using Microsoft.AspNetCore.Mvc;
using System.Data;
using VetApp_BE.Feature.Appointment.Models;
using VetApp_BE.Feature.Appointment.Repositories;
using VetApp_BE.Feature.Appointment.ViewModels;
using VetApp_BE.Feature.Pets;
using VetApp_BE.Feature.Pets.Repositories;
using VetApp_BE.Feature.Pets.ViewModels;
using VetApp_BE.Helpers;

namespace VetApp_BE.Feature.Appointment
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPetRepository _petRepository;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentRepository appointmentRepository, IPetRepository petRepository, ILogger<AppointmentController> logger )
        {
            _appointmentRepository = appointmentRepository;
            _petRepository = petRepository;
            _logger = logger;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<List<AppointmentModels>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] AppointmentSearch search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = new ApiResponsePaging<List<AppointmentModels>>();

            try
            {
                var data = await _appointmentRepository.GetAppointments(search, pageNumber, pageSize);
                var Totaldata = await _appointmentRepository.GetAppointmentsTotal(search);
                
                
                response.page_number = pageNumber;
                response.page_size = pageSize;
                response.total_record = Totaldata;

                response.data = data.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<AppointmentModels>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AppointmentRequestViewModels request)
        {
            var response = new ApiResponse<AppointmentModels>();

            try
            {
                if (request.is_new_pats)
                {
                    var data_new = ObjectHelpers.Convert<AppointmentRequestViewModels, AppointmentModels>(request);
                    response.data = await _appointmentRepository.AddAsync(data_new);
                    return Ok(response);
                }

                var dt_pet_old = await _petRepository.GetByIdAsync(Convert.ToInt32(request.Pet.Id));
                var data_vat_old = ObjectHelpers.Convert<AppointmentRequestViewModels, AppointmentModels>(request);
                data_vat_old.Pet = dt_pet_old;

                response.data = await _appointmentRepository.AddAsync(data_vat_old);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
         
        }
    }

    
}
