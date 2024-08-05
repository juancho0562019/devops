
using Bext.Reps.Business.Features.Complejidades;
using Bext.Reps.Business.Features.Complejidades.Queries.Get;
using Bext.Reps.Business.Features.Complejidades.Queries.GetAll;
using Bext.Reps.Business.Features.Especificidades;
using Bext.Reps.Business.Features.Especificidades.Queries.Get;
using Bext.Reps.Business.Features.Especificidades.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplejidadController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ComplejidadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetComplejidadRequest complejidadRequest)
    {
        var result = await sender.Send(complejidadRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<ComplejidadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllComplejidadRequest getAllComplejidadRequest)
    {
        var result = await sender.Send(getAllComplejidadRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
