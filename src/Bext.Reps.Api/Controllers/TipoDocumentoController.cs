using Bext.Reps.Business.Features.ClasePrestadores;
using Bext.Reps.Business.Features.TiposDocumentos.Queries.Get;
using Bext.Reps.Business.Features.TiposDocumentos.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoDocumentoController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ClasePrestadorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetTipoDocumentoRequest tipoDocumentoAllRequest)
    {
        var result = await sender.Send(tipoDocumentoAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<ClasePrestadorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllTipoDocumentoRequest tipoDocumentoAllRequest)
    {
        var result = await sender.Send(tipoDocumentoAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
