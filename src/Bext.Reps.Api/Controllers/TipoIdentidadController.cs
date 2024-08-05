using Bext.Reps.Business.Features.TipoIdentidades;
using Bext.Reps.Business.Features.TipoIdentidades.Queries.Get;
using Bext.Reps.Business.Features.TipoIdentidades.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoIdentidadController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(TipoIdentidadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetTipoIdentidadRequest tipoIdentidadRequest)
    {
        var result = await sender.Send(tipoIdentidadRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<TipoIdentidadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetTipoIdentidadAllRequest tipoIdentidadAllRequest)
    {
        var result = await sender.Send(tipoIdentidadAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
