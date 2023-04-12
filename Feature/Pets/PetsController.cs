using Microsoft.AspNetCore.Mvc;

namespace VetApp_BE.Feature.Pets
{
    [ApiController]
    [Route("[controller]")]
    public class Pets : ControllerBase
    {
        public Pets()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok("ok");
        }
    }
}
