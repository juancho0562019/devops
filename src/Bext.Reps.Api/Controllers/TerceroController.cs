using Bext.Reps.Business.Features.Terceros;
using Bext.Reps.Business.Features.Terceros.Commands.Create;
using Bext.Reps.Business.Features.Terceros.Commands.Update;
using Bext.Reps.Business.Features.Terceros.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TerceroController : ControllerBase
{
    private readonly ILogger<TerceroController> _logger;

    public TerceroController(ILogger<TerceroController> logger)
    {
        _logger = logger;
    }

    [HttpPost("terceros")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTercero(ISender sender, CreateTerceroCommand terceroRequest)
    {
        var result = await sender.Send(terceroRequest);
        return Ok(result);
    }

    [HttpPatch("terceros/{id}")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTercero(int id, ISender sender, UpdateTerceroCommand terceroRequest)
    {
        if (id != terceroRequest.Id)
        {
            return BadRequest(new ProblemDetails { Title = "ID in URL does not match ID in request body." });
        }

        await sender.Send(terceroRequest);
        return Ok(Unit.Value);
    }


    [HttpGet("terceros/{numeroDocumento}")]
    [ProducesResponseType(typeof(TerceroDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTerceroByNumeroDocumento(string numeroDocumento, ISender sender)
    {
        var tercero = await sender.Send(new GetTerceroRequest() { NumeroDocumento = numeroDocumento});
        if (tercero is null)
        {
            return NotFound(new ProblemDetails { Title = "Tercero not found." });
        }

        return Ok(tercero);
    }
}
