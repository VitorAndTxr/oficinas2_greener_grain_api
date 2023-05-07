using Microsoft.AspNetCore.Mvc;

namespace GreenerGrain.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok();
        }
    }
   
}
