

using MediatR;


namespace Bext.Reps.Business.Features.Terceros.Commands.Create
{
    public record CreateTerceroCommand(
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
    string? Email) : IRequest<int>;

    public class CreateTerceroCommandHandler : IRequestHandler<CreateTerceroCommand, int>
    {
        private readonly IRepsDbContext _context;

        public CreateTerceroCommandHandler(IRepsDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTerceroCommand request, CancellationToken cancellationToken)
        {
            var identificacionEntity = await _context.DocumentosIdentidad.FindAsync(request.TipoIdentificacion, cancellationToken);

            if (identificacionEntity is null)
                throw new NotFoundException(nameof(request.TipoIdentificacion), request.TipoIdentificacion.ToString());

            var identificacion = Identificacion.Crear(request.TipoIdentificacion, request.NumeroIdentificacion);

            var direccion = Ubicacion.Crear(request.Pais, request.Departamento, request.Municipio);

            var tercero = new Tercero.Builder()
                .ConIdentificacion(identificacion)
                .ConNombres(request.PrimerNombre, request.SegundoNombre, request.PrimerApellido, request.SegundoApellido)
                .ConRazonSocial(request.RazonSocial)
                .ConUbicacion(direccion)
                .ConContactos(request.TelefonoFijo, request.TelefonoMovil, request.TelefonoFax, request.SitioWeb, request.Email)
                .Build();

            await _context.Terceros.AddAsync(tercero);
            await _context.SaveChangesAsync(cancellationToken);

            return tercero.Id;
        }
    }
}
