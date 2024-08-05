using Bext.Reps.Business.Features.GrupoServicios;
using Bext.Reps.Business.Features.GrupoServicios.Queries.Get;
using Bext.Reps.Business.Features.GrupoServicios.Queries.GetAll;
using Bext.Reps.Business.Features.TipoPersonas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GrupoServicioController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(GrupoServicioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetGrupoServicioRequest grupoServicioRequest)
    {
        var result = await sender.Send(grupoServicioRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<TipoPersonaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllGrupoServiciosRequest getAllGrupoServicioRequest)
    {
        var result = await sender.Send(getAllGrupoServicioRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
