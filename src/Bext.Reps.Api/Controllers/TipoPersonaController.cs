using Bext.Reps.Business.Features.TipoPersonas;
using Bext.Reps.Business.Features.TipoPersonas.Queries.Get;
using Bext.Reps.Business.Features.TipoPersonas.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoPersonaController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(TipoPersonaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetTipoPersonaRequest tipoPersonaRequest)
    {
        var result = await sender.Send(tipoPersonaRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<TipoPersonaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetTipoPersonaAllRequest tipoPersonaAllRequest)
    {
        var result = await sender.Send(tipoPersonaAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
