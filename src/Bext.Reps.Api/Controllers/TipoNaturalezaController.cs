using Bext.Reps.Business.Features.TipoNaturalezas;
using Bext.Reps.Business.Features.TipoNaturalezas.Queries.Get;
using Bext.Reps.Business.Features.TipoNaturalezas.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoNaturalezaController : ControllerBase
{
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(TipoNaturalezaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetTipoNaturalezaRequest tipoNaturalezaRequest)
    {
        var result = await sender.Send(tipoNaturalezaRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<TipoNaturalezaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetTipoNaturalezaAllRequest tipoNaturalezaAllRequest)
    {
        var result = await sender.Send(tipoNaturalezaAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
