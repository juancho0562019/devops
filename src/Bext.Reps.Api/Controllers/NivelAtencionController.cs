using Bext.Reps.Business.Features.NivelesAtencion;
using Bext.Reps.Business.Features.NivelesAtencion.Queries.Get;
using Bext.Reps.Business.Features.NivelesAtencion.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NivelAtencionController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(NivelAtencionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetNivelAtencionRequest tipoPersonaRequest)
    {
        var result = await sender.Send(tipoPersonaRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<NivelAtencionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllNivelAtencionRequest tipoPersonaAllRequest)
    {
        var result = await sender.Send(tipoPersonaAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
