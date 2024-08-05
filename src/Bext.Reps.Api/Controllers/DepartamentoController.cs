using Bext.Reps.Business.Commons.Interfaces.Repository;
using Bext.Reps.Business.Features.Departamentos;
using Bext.Reps.Business.Features.Departamentos.Queries.Get;
using Bext.Reps.Business.Features.Departamentos.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentosController : ControllerBase
{
 

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(DepartamentoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetDepartamentosQuery departamentosQuery)
    {
        var result = await sender.Send(departamentosQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<DepartamentoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllDepartamentosQuery departamentosQuery)
    {
        var result = await sender.Send(departamentosQuery);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
