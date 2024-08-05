
using Bext.Reps.Business.Features.Solicitudes.Commands.AgregarDocumentos;
using Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios;
using Bext.Reps.Business.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudController : ControllerBase
{
    private readonly ILogger<SolicitudController> _logger;

    public SolicitudController(ILogger<SolicitudController> logger)
    {
        _logger = logger;
    }


    [HttpPost("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RegistrarServicio(ISender sender, [FromBody] AgregarServiciosRequest agregarServicioRequest)
    {
        var result = await sender.Send(agregarServicioRequest);
        return result.IsSuccess ? StatusCode(StatusCodes.Status200OK, result.Value) : (ActionResult)BadRequest(result.Error);
    }

    [HttpPatch("{id}/RegistrarDocumentoServicio")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AgregarDocumentoServicio(int id, ISender sender, [FromForm] AgregarDocumentoServicioRequest addDocumentoRequest)
    {
        if (id != addDocumentoRequest.SolicitudId)
        {
            return BadRequest("El id de la solicitud en la ruta no coincide con el id de la solicitud en el cuerpo de la solicitud.");
        }

        var result = await sender.Send(addDocumentoRequest);
        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status200OK, result);
        }

        return BadRequest(result);
    }
}
