using Bext.Reps.Business.Features.Departamentos.Queries.Get;
using Bext.Reps.Business.Features.Departamentos.Queries.GetAll;
using Bext.Reps.Business.Features.Municipios;
using Bext.Reps.Business.Features.Municipios.Queries.Get;
using Bext.Reps.Business.Features.Municipios.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MunicipiosController : ControllerBase
{


    [HttpGet("[action]")]
    [ProducesResponseType(typeof(MunicipioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetMunicipiosQuery departamentosQuery)
    {
        var result = await sender.Send(departamentosQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<MunicipioDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllMunicipiosQuery departamentosQuery)
    {
        var result = await sender.Send(departamentosQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
