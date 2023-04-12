using Microsoft.AspNetCore.Mvc;

namespace VetApp_BE.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        public HomeController()
        {
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> Get()
        {
            var id = System.Environment.MachineName;
            var pid = System.Diagnostics.Process.GetCurrentProcess().Id;
            return Ok("Vet Api Service Runnning," + id + "," + pid);
        }


        [HttpGet]
        [Route("/health")]
        public async Task<IActionResult> getHealth()
        {

            var id = System.Environment.MachineName;
            var pid = System.Diagnostics.Process.GetCurrentProcess().Id;
            return Ok("Vet Api Service Runnning," + id + "," + pid);
        }
    }
}
