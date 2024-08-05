using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.CentrosPoblados;
using Bext.Reps.Business.Features.CentrosPoblados.Queries.Get;
using Bext.Reps.Business.Features.CentrosPoblados.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CentrosPobladoController : ControllerBase
{
 

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(CentroPobladoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetAllCentroPobladoQuery centrosQuery)
    {
        var result = await sender.Send(centrosQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<CentroPobladoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllCentroPobladoQuery centrosQuery)
    {
        var result = await sender.Send(centrosQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
