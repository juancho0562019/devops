using Bext.Reps.Business.Features.TipoNaturalezas.Queries.Get;
using Bext.Reps.Business.Features.TipoNaturalezas.Queries.GetAll;
using Bext.Reps.Business.Features.ClasePrestadores.Queries.Get;
using Bext.Reps.Business.Features.ClasePrestadores.Queries.GetAll;
using Bext.Reps.Domain.Commons.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Bext.Reps.Domain.Entities;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoVinculacionController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(TipoVinculacion), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetTipoVinculacionRequest tipoVinculacionRequest)
    {
        var result = await sender.Send(tipoVinculacionRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(TipoVinculacion), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetTipoVinculacionAllRequest tipoVinculacionAllRequest)
    {
        var result = await sender.Send(tipoVinculacionAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
