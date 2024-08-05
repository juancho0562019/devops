using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Barrios;
using Bext.Reps.Business.Features.Barrios.Queries.Get;
using Bext.Reps.Business.Features.Barrios.Queries.GetAll;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BarriosController : ControllerBase
{
 

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(BarrioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetBarrioQuery barrioQuery)
    {
        var result = await sender.Send(barrioQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<BarrioDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllBarrioQuery barrioQuery)
    {
        var result = await sender.Send(barrioQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
