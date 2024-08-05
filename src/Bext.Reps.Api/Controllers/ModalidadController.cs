using Bext.Reps.Business.Features.Modalidades;
using Bext.Reps.Business.Features.Modalidades.Queries.Get;
using Bext.Reps.Business.Features.Modalidades.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModalidadController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ModalidadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetModalidadRequest getModalidadRequest)
    {
        var result = await sender.Send(getModalidadRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<ModalidadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllModalidadRequest getAllModalidad)
    {
        var result = await sender.Send(getAllModalidad);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
