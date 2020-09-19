using Microsoft.AspNetCore.Mvc;

namespace AppUnderTest.Controllers
{
    [ApiController]
    [Route("ready")]
    public class ReadyController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Ready");
        }
    }
}
