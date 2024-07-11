using MediatR;

namespace Bext.Reps.Business.Features.Terceros.Commands.Update;
public record UpdateTerceroCommand(
    int Id,
    string TipoIdentificacion,
    string NumeroIdentificacion,
    string PrimerNombre,
    string? SegundoNombre,
    string PrimerApellido,
    string? SegundoApellido,
    string? RazonSocial,
    string Pais,
    string Departamento,
    string Municipio,
    string? TelefonoFijo,
    string? TelefonoMovil,
    string? TelefonoFax,
    string? SitioWeb,
    string? Email) : IRequest<Unit>;

public class UpdateTerceroCommandHandler : IRequestHandler<UpdateTerceroCommand, Unit>
{
    private readonly IRepsDbContext _context;

    public UpdateTerceroCommandHandler(IRepsDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTerceroCommand request, CancellationToken cancellationToken)
    {
        var tercero = await _context.Terceros.FindAsync(request.Id);

        if (tercero is null)
            throw new NotFoundException(nameof(Tercero), request.Id);
        bool changesMade = false;
      

        if (!string.IsNullOrEmpty(request.TipoIdentificacion) || !string.IsNullOrEmpty(request.NumeroIdentificacion))
        {
            var identificacionEntity = await _context.DocumentosIdentidad.FindAsync(request.TipoIdentificacion, cancellationToken);

            if (identificacionEntity is null)
                throw new NotFoundException(nameof(request.TipoIdentificacion), request.TipoIdentificacion.ToString());

            var nuevaIdentificacion = Identificacion.Crear(
                string.IsNullOrEmpty(request.TipoIdentificacion) ? tercero.Identificacion.TipoIdentificacionId : request.TipoIdentificacion,
                string.IsNullOrEmpty(request.NumeroIdentificacion) ? tercero.Identificacion.NumeroDocumento : request.NumeroIdentificacion
            );
            if (!tercero.Identificacion.Equals(nuevaIdentificacion))
            {
                tercero.ActualizarIdentificacion(nuevaIdentificacion);
                changesMade = true;
            }
        }


        if (request.PrimerNombre != tercero.PrimerNombre ||
            request.SegundoNombre != tercero.SegundoNombre ||
            request.PrimerApellido != tercero.PrimerApellido ||
            request.SegundoApellido != tercero.SegundoApellido)
        {
            tercero.ActualizarNombres(
                string.IsNullOrEmpty(request.PrimerNombre) ? tercero.PrimerNombre : request.PrimerNombre,
                string.IsNullOrEmpty(request.SegundoNombre) ? tercero.SegundoNombre : request.SegundoNombre,
                string.IsNullOrEmpty(request.PrimerApellido) ? tercero.PrimerApellido : request.PrimerApellido,
                string.IsNullOrEmpty(request.SegundoApellido) ? tercero.SegundoApellido : request.SegundoApellido
            );
            changesMade = true;
        }

        var nuevaUbicacion = Ubicacion.Crear(
            string.IsNullOrEmpty(request.Pais) ? tercero.Ubicacion.Pais : request.Pais,
            string.IsNullOrEmpty(request.Departamento) ? tercero.Ubicacion.Departamento : request.Departamento,
            string.IsNullOrEmpty(request.Municipio) ? tercero.Ubicacion.Municipio : request.Municipio
        );

        if (!tercero.Ubicacion.Equals(nuevaUbicacion))
        {
            tercero.ActualizarUbicacion(nuevaUbicacion);
            changesMade = true;
        }

        if (request.TelefonoFijo != tercero.TelefonoFijo ||
            request.TelefonoMovil != tercero.TelefonoMovil ||
            request.TelefonoFax != tercero.TelefonoFax ||
            request.SitioWeb != tercero.SitioWeb ||
            request.Email != tercero.Email)
        {
            tercero.ActualizarContactos(
                string.IsNullOrEmpty(request.TelefonoFijo) ? tercero.TelefonoFijo : request.TelefonoFijo,
                string.IsNullOrEmpty(request.TelefonoMovil) ? tercero.TelefonoMovil : request.TelefonoMovil,
                string.IsNullOrEmpty(request.TelefonoFax) ? tercero.TelefonoFax : request.TelefonoFax,
                string.IsNullOrEmpty(request.SitioWeb) ? tercero.SitioWeb : request.SitioWeb,
                string.IsNullOrEmpty(request.Email) ? tercero.Email : request.Email
            );
            changesMade = true;
        }
        if (!string.IsNullOrEmpty(request.RazonSocial) && request.RazonSocial != tercero.RazonSocial)
        {
            tercero.ActualizarRazonSocial(request.RazonSocial);
            changesMade = true;
        }


        if (changesMade)
        {
            _context.Terceros.Update(tercero);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }

   
}
