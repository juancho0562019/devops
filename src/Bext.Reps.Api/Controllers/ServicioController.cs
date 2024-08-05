using MediatR;
using Microsoft.AspNetCore.Mvc;
using Bext.Reps.Business.Features.Servicios;
using Bext.Reps.Business.Features.Servicios.Queries.Get;
using Bext.Reps.Business.Features.Servicios.Queries.GetAll;
using Bext.Reps.Business.Features.Servicios.Queries.GetAllEstandar;
using Bext.Reps.Business.Features.Estandares.Queries.GetAll;

namespace Bext.Reps.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(ServicioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetServicioRequest servicioRequest)
        {
            var result = await sender.Send(servicioRequest);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<ServicioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllServicioRequest getAllServicioRequest)
        {
            var result = await sender.Send(getAllServicioRequest);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<EstandarServicioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEstandarServicio(ISender sender, [FromQuery] GetAllEstandarServicioRequest getAllServicioRequest)
        {
            var result = await sender.Send(getAllServicioRequest);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }

}
