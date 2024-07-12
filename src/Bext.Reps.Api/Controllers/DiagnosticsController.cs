namespace Bext.Reps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DiagnosticsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("settings")]
        public IActionResult GetSettings()
        {
            var settings = _configuration.GetSection("ConnectionStrings").Value;
            return Ok(new { ConnectionString = settings });
        }
    }
}