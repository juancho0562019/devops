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
            var settings = _configuration.GetSection("ConnectionStrings").Value;
            return Ok(new { ConnectionString = settings });
        }
    }
}