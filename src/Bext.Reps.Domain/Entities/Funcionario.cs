using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Domain.Entities;

public sealed class Funcionario : BaseEntity<Guid>
{
    public required string Nombre { get; init; }

    public int? EntidadId { get; private set; }

    public int? CodigoInterno { get; private set; }

    public Guid RolAplicacionId { get; private set; }

    public required string Email { get; init; }

    public RefreshToken? RefreshToken { get; private set; }

    public RolAplicacion? RolAplicacion { get; private set; }

    public static Funcionario Crear(string nombre, int? entidadId, int? codigoInterno, string email,
                                    RefreshToken refreshToken, RolAplicacion rolAplicacion)
    {
        ArgumentException.ThrowIfNullOrEmpty(nombre);
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentNullException.ThrowIfNull(refreshToken);
        ArgumentNullException.ThrowIfNull(rolAplicacion);
        if (rolAplicacion.EsInterno)
        {
            ArgumentNullException.ThrowIfNull(codigoInterno);
        }
        else
        {
            ArgumentNullException.ThrowIfNull(entidadId);
        }

        var funcionario = new Funcionario
        {
            Nombre = nombre,
            EntidadId = entidadId,
            Email = email,
            RefreshToken = refreshToken,
            RolAplicacion = rolAplicacion,
            CodigoInterno = codigoInterno
        };

        return funcionario;
    }

    public void UpdateRefreshToken(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);
        RefreshToken = refreshToken;
    }

}