using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Primitives;
using Bext.Reps.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bext.Reps.Domain.Entities;

public sealed class Contacto : BaseEntity<int>
{
    public required TipoContacto TipoContacto { get; init; }
    public required Nombre Nombre { get; init; }
    public required Identificacion Identificacion { get; init; }
    public required string Telefono { get; init; }
    public required string CorreoInstitucional { get; init; }
    public TipoRepresentanteLegal? TipoRepresentanteLegal { get; init; }
    public string? Profesion { get; init; }
    public string? TarjetaProfesional { get; init; }
    public string? InformacionOficio { get; init; }
    public DateTime? FechaDocumentoAutorizacion { get; init; }

    public int EntidadId { get; init; }
    public Entidad? Entidad { get; set; }

    private static Contacto Crear(TipoContacto tipoContacto, Nombre nombre, Identificacion identificacion,
                string telefono, string correoInstitucional, TipoRepresentanteLegal? tipoRepresentanteLegal = null,
                string? profesion = null, string? tarjetaProfesional = null, string? informacionOficio = null, DateTime? fechaDocumentoAutorizacion = null)
    {
        ArgumentNullException.ThrowIfNull(tipoContacto, nameof(tipoContacto));
        ArgumentNullException.ThrowIfNull(nombre, nameof(Nombre));
        ArgumentNullException.ThrowIfNull(identificacion, nameof(identificacion));
        ArgumentException.ThrowIfNullOrEmpty(telefono, nameof(telefono));
        ArgumentException.ThrowIfNullOrEmpty(correoInstitucional, nameof(correoInstitucional));

        var contacto = new Contacto
        {
            TipoContacto = tipoContacto,
            Nombre = nombre,
            Identificacion = identificacion,
            Telefono = telefono,
            CorreoInstitucional = correoInstitucional,
            TipoRepresentanteLegal = tipoRepresentanteLegal,
            Profesion = profesion,
            TarjetaProfesional = tarjetaProfesional,
            InformacionOficio = informacionOficio,
            FechaDocumentoAutorizacion = fechaDocumentoAutorizacion
        };

        return contacto;
    }

    public static Contacto CrearPrincipal(Nombre nombre, Identificacion identificacion, string telefono, string correoInstitucional)
    {
        return Crear(TipoContacto.Principal, nombre, identificacion, telefono, correoInstitucional);
    }

    public static Contacto CrearRepresentanteLegal(Nombre nombre, Identificacion identificacion, string telefono, string correoInstitucional, TipoRepresentanteLegal? tipoRepresentanteLegal)
    {
        ArgumentNullException.ThrowIfNull(tipoRepresentanteLegal, nameof(tipoRepresentanteLegal));

        return Crear(TipoContacto.RepresentanteLegal, nombre, identificacion, telefono, correoInstitucional, tipoRepresentanteLegal);
    }

    public static Contacto CrearApoderado(Nombre nombre, Identificacion identificacion, string telefono, string correoInstitucional, string? informacionOficio, DateTime? fechaDocumentoAutorizacion)
    {
        ArgumentException.ThrowIfNullOrEmpty(informacionOficio, nameof(informacionOficio));
        ArgumentNullException.ThrowIfNull(fechaDocumentoAutorizacion, nameof(fechaDocumentoAutorizacion));

        return Crear(TipoContacto.Apoderado, nombre, identificacion, telefono, correoInstitucional, informacionOficio: informacionOficio, fechaDocumentoAutorizacion: fechaDocumentoAutorizacion);
    }

    public static Contacto CrearDirectorTecnico(Nombre nombre, Identificacion identificacion, string telefono, string correoInstitucional, string? profesion, string? tarjetaProfesional)
    {
        ArgumentException.ThrowIfNullOrEmpty(profesion, nameof(profesion));
        ArgumentException.ThrowIfNullOrEmpty(tarjetaProfesional, nameof(tarjetaProfesional));

        return Crear(TipoContacto.DirectorTecnico, nombre, identificacion, telefono, correoInstitucional, profesion: profesion, tarjetaProfesional: tarjetaProfesional);
    }

}


