using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Primitives;
using Bext.Reps.Domain.ValueObjects;

namespace Bext.Reps.Domain.Entities;

public sealed class Sede : BaseEntity<int>
{
    public required string Nombre { get; init; }

    public required string MatriculaMercantil { get; init; }

    public required string CodigoSede { get; init; }



    public required Ubicacion Ubicacion { get; init; }

    public required string DireccionNotificacionJudicial { get; init; }

    public required string CorreoElectronico { get; init; }

    public required string Telefono { get; init; }

    public required string RegistroMercantil { get; init; }

    public int EntidadId { get; set; }

    public Entidad? Entidad { get; set; }

    public static Sede Crear(string nombre, string matriculaMercantil, string codigoSede, TipoPrestador tipoPrestador,
        Ubicacion ubicacion, string direccionNotificacionJudicial, string correoElectronico, string telefono, string registroMercantil)
    {
        ArgumentNullException.ThrowIfNull(tipoPrestador);
        ArgumentNullException.ThrowIfNull(ubicacion);
        ArgumentException.ThrowIfNullOrEmpty(nombre);
        ArgumentException.ThrowIfNullOrEmpty(matriculaMercantil);
        ArgumentException.ThrowIfNullOrEmpty(codigoSede);
        ArgumentException.ThrowIfNullOrEmpty(direccionNotificacionJudicial);
        ArgumentException.ThrowIfNullOrEmpty(correoElectronico);
        ArgumentException.ThrowIfNullOrEmpty(telefono);
        ArgumentException.ThrowIfNullOrEmpty(registroMercantil);

        var sede = new Sede
        {
            Nombre = nombre,
            MatriculaMercantil = matriculaMercantil,
            CodigoSede = codigoSede,
            Ubicacion = ubicacion,
            DireccionNotificacionJudicial = direccionNotificacionJudicial,
            CorreoElectronico = correoElectronico,
            Telefono = telefono,
            RegistroMercantil = registroMercantil
        };

        return sede;
    }
}


