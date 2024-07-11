using System.Runtime.CompilerServices;

namespace Bext.Reps.Domain.ViewModel;

public class RolResponse
{
    public required Guid Id { get; init; }
    public required string Nombre { get; init; }
    public required bool EsInterno { get; init; }
}