using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Zonas;
using Bext.Reps.Business.Features.Zonas.Queries.Get;
using Bext.Reps.Business.Features.Zonas.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZonasController : ControllerBase
{
 

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ZonaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetZonaQuery zonasQuery)
    {
        var result = await sender.Send(zonasQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<ZonaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllZonaQuery zonasQuery)
    {
        var result = await sender.Send(zonasQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
