using Bext.Reps.Business.Features.ClasePrestadores;
using Bext.Reps.Business.Features.ClasePrestadores.Queries.Get;
using Bext.Reps.Business.Features.ClasePrestadores.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClasePrestadorController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ClasePrestadorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetClasePrestadorRequest tipoPrestadorRequest)
    {
        var result = await sender.Send(tipoPrestadorRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ClasePrestadorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetClasePrestadorAllRequest tipoPrestadorAllRequest)
    {
        var result = await sender.Send(tipoPrestadorAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
