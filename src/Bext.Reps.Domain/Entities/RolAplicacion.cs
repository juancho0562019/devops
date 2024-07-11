using Bext.Reps.Domain.Primitives;
using Bext.Reps.Domain.ViewModel;

using System.Runtime.CompilerServices;

namespace Bext.Reps.Domain.Entities;

public sealed class RolAplicacion : BaseEntity<Guid>
{
    public required string Nombre { get; init; }
    public required bool EsInterno { get; init; }
    public List<Funcionario> Funcionarios { get; private set; } = [];

    public static RolAplicacion Crear(string nombre, bool esInterno)
    {
        ArgumentException.ThrowIfNullOrEmpty(nombre, nameof(nombre));
        ArgumentNullException.ThrowIfNull(esInterno, nameof(esInterno));

        var rolAplicacion = new RolAplicacion
        {
            Nombre = nombre,
            EsInterno = esInterno
        };
        return rolAplicacion;
    }
}