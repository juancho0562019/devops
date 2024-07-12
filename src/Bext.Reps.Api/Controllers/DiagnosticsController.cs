using Bext.Reps.Business.Features.Terceros;
using Bext.Reps.Business.Features.Terceros.Commands.Create;
using Bext.Reps.Business.Features.Terceros.Commands.Update;
using Bext.Reps.Business.Features.Terceros.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            var connectionString = _configuration.GetConnectionString("RepsConnectionString");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return Ok(new { ConnectionString = connectionString, Environment = environment });
        }
    }
}