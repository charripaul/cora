using Microsoft.AspNetCore.Mvc;

namespace CoraBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public IActionResult Test()
        {
            return this.Ok("Test successful");
        }
    }
}
