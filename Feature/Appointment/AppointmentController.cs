using Microsoft.AspNetCore.Mvc;

namespace VetApp_BE.Feature.Appointment
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        public AppointmentController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
          
            return Ok("ok");
        }
    }
}
