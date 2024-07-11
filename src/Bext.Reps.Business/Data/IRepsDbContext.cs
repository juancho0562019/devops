using Microsoft.EntityFrameworkCore;
using Bext.Reps.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Bext.Reps.Domain.Primitives;

namespace Bext.Reps.Business.Data;

public interface IRepsDbContext
{
    public DbSet<Contacto> Contactos { get;  }
    public DbSet<Documento> Documentos { get;  }
    public DbSet<DocumentoEntidad> DocumentosEntidad { get;  }
    public DbSet<Entidad> Entidades { get;  }
    public DbSet<Funcionario> Funcionarios { get;  }
    public DbSet<Modalidad> Modalidades { get;  }
    public DbSet<RegistroModalidad> RegistroModalidades { get;  }
    public DbSet<RolAplicacion> RolesAplicacion { get;  }
    public DbSet<Sede> Sedes { get;  }
    public DbSet<Tercero> Terceros { get;  }
    public DbSet<DocumentoIdentidad> DocumentosIdentidad { get;  }
    public DbSet<TipoDocumento> TiposDocumentos { get;  }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry Entry<T, TKey>(T entity) where T : BaseEntity<TKey> where TKey : notnull;
}
