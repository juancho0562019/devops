using Bext.Reps.Business.Features.TipoNaturalezas.Queries.Get;
using Bext.Reps.Business.Features.TipoNaturalezas.Queries.GetAll;
using Bext.Reps.Business.Features.ClasePrestadores.Queries.Get;
using Bext.Reps.Business.Features.ClasePrestadores.Queries.GetAll;
using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaracterTerritorialController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(CaracterTerritorial), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetCaracterTerritorialRequest caracterTerritorialRequest)
    {
        var result = await sender.Send(caracterTerritorialRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(CaracterTerritorial), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetCaracterTerritorialAllRequest caracterTerritorialAllRequest)
    {
        var result = await sender.Send(caracterTerritorialAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
