using Bext.Reps.Business.Features.TipoPersonas;
using Bext.Reps.Business.Features.TipoPersonas.Queries.Get;
using Bext.Reps.Business.Features.TipoPersonas.Queries.GetAll;
using Bext.Reps.Business.Features.TiposDocumentos;
using Bext.Reps.Business.Features.TiposDocumentos.Queries.Get;
using Bext.Reps.Business.Features.TiposDocumentos.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bext.Reps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentoConstitucionController : ControllerBase
{
    

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(DocumentoConstitucionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(ISender sender, [FromQuery] GetDocumentoConstitucionRequest documentoConstitucionRequest)
    {
        var result = await sender.Send(documentoConstitucionRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<DocumentoConstitucionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(ISender sender, [FromQuery] GetDocumentoConstitucionAllRequest documentoConstitucionAllRequest)
    {
        var result = await sender.Send(documentoConstitucionAllRequest);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
