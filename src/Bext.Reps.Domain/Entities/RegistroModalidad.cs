using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;

public sealed class RegistroModalidad : BaseEntity<int>
{
    public int ModalidadId { get; private set; }
    public required DateTime Fecha { get; init; }
    public Entidad Entidad { get; private set; } = null!;
    public EstadoRegistro Estado { get; private set; }
    public Guid FuncionarioInternoId { get; set; }

    public Guid FuncionarioExternoId { get; set; }
    public int EntidadId { get; private set; }
    public required Modalidad Modalidad { get; init; }
    public IEnumerable<Documento>? Documentos { get; private set; } = [];
    public Funcionario? FuncionarioInterno { get; private set; }
    public required Funcionario FuncionarioExterno { get; init; }

    public static RegistroModalidad Create(Modalidad modalidad, DateTime fecha, Entidad entidad, IEnumerable<Documento> documentos, Funcionario funcionarioExterno)
    {
        ArgumentNullException.ThrowIfNull(modalidad, nameof(modalidad));
        ArgumentNullException.ThrowIfNull(fecha, nameof(fecha));
        ArgumentNullException.ThrowIfNull(entidad, nameof(entidad));
        ArgumentNullException.ThrowIfNull(funcionarioExterno, nameof(funcionarioExterno));

        var registroModalidad = new RegistroModalidad
        {
            Modalidad = modalidad,
            Fecha = fecha,
            Entidad = entidad,
            Estado = EstadoRegistro.Creado,
            FuncionarioExterno = funcionarioExterno,
            Documentos = documentos ?? new List<Documento>()
        };

        return registroModalidad;                       
    }

    public void AsignarFuncionario(Funcionario funcionario)
    {
        ArgumentNullException.ThrowIfNull(funcionario, nameof(funcionario));
        if (funcionario.RolAplicacion is null || !funcionario.RolAplicacion.EsInterno)
    {
            throw new InvalidOperationException("El funcionario asignado debe ser interno");
        }

        FuncionarioInterno = funcionario;
        AsignarEstadoRegistro(EstadoRegistro.Asignado);
    }

    public void AsignarEstadoRegistro(EstadoRegistro estado)
    {
        Estado = estado;
    }

}