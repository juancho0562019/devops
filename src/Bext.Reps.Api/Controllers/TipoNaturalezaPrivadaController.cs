using Bext.Reps.Business.Features.SubTipoNaturalezas.Queries.GetAll;
using Bext.Reps.Business.Features.TipoNaturalezasPrivadas;
using Bext.Reps.Business.Features.TipoNaturalezasPrivadas.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubTipoNaturalezaController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(SubTipoNaturalezaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetSubTipoNaturalezaRequest tipoNaturalezaPrivadaRequest)
    {
        var result = await sender.Send(tipoNaturalezaPrivadaRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<SubTipoNaturalezaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetSubTipoNaturalezaAllRequest tipoNaturalezaPrivadaAllRequest)
    {
        var result = await sender.Send(tipoNaturalezaPrivadaAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
