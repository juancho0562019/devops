
using Bext.Reps.Business.Features.Criterios.Queries.GetAll;
using Bext.Reps.Business.Features.Criterios;
using Bext.Reps.Business.Features.Entidades.Commands.AgregarDocumentos;
using Bext.Reps.Business.Features.Entidades.Commands.AgregarSede;
using Bext.Reps.Business.Features.Entidades.Commands.Create;
using Bext.Reps.Business.Features.Solicitudes.Commands.AgregarServicios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Bext.Reps.Business.Features.Entidades.Queries;
using Bext.Reps.Business.Commons.Models;
using Bext.Reps.Business.Features.Entidades.Queries.GetAll;
using Bext.Reps.Business.Features.Entidades.Commands.AgregarDocumentosSede;
using Bext.Reps.Business.Models;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntidadController : ControllerBase
{
    private readonly ILogger<EntidadController> _logger;

    public EntidadController(ILogger<EntidadController> logger)
    {
        _logger = logger;
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(ISender sender, CreateEntidadCommand terceroRequest)
    {
        var result = await sender.Send(terceroRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
    [HttpPatch("{id}/RegistrarSede")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AgregarSede(int id, ISender sender, [FromBody] AgregarSedeRequest addSedeRequest)
    {
        if (id != addSedeRequest.EntidadId)
        {
            return BadRequest("El id de la entidad en la ruta no coincide con el id de la entidad en el cuerpo de la solicitud.");
        }

        var result = await sender.Send(addSedeRequest);
        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status200OK, result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpPatch("{id}/RegistrarDocumentoPrestador")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AgregarDocumento(int id, ISender sender,[FromForm] AgregarDocumentoRequest addDocumentoRequest)
    {
        if (id != addDocumentoRequest.EntidadId)
        {
            return BadRequest("El id de la entidad en la ruta no coincide con el id de la entidad en el cuerpo de la solicitud.");
        }

        var result = await sender.Send(addDocumentoRequest);
        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status200OK, result);
        }

        return BadRequest(result);
    }
    [HttpPatch("{id}/RegistrarDocumentoSede/{idSede}")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AgregarDocumentoSede(int id, int idSede, ISender sender, [FromForm] AgregarDocumentoSedeRequest addDocumentoRequest)
    {
        if (id != addDocumentoRequest.EntidadId)
        {
            return BadRequest("El id de la entidad en la ruta no coincide con el id de la entidad en el cuerpo de la solicitud.");
        }

        if (idSede != addDocumentoRequest.SedeId)
        {
            return BadRequest("El id de la sede en la ruta no coincide con el id de la sede en el cuerpo de la solicitud.");
        }

        var result = await sender.Send(addDocumentoRequest);
        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status200OK, result);
        }

        return BadRequest(result);
    }

   



    [HttpGet("[action]")]
    [ProducesResponseType(typeof(PaginatedList<EntidadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetAllEntidadRequest getAllEntidadRequest)
    {
        var result = await sender.Send(getAllEntidadRequest);
        
        return Ok(result);
        
    }
}
