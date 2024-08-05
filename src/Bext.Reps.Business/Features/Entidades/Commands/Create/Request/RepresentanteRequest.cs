
using System.ComponentModel.DataAnnotations;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;

namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
public record RepresentanteRequest(
                                   [Required(ErrorMessage = DefaultMessage.IsRequired)]
                                   string TipoIdentificacion, 
                                   string NumeroIdentificacion,
                                   string PrimerNombre,
                                   string? SegundoNombre,
                                   string PrimerApellido,
                                   string? SegundoApellido,
                                   TipoRepresentacion TipoRepresentacion,
                                   DateTime FechaInicioRepresentacion,
                                   string? TipoVinculacion,
                                   DateTime? FechaVinculacion);
