namespace Bext.Reps.Domain.Entities.DirectorioGeneral;
public class ItemTablaReferencia
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Habilitado { get; set; }
    public string? Extra_I { get; set; }
}
public class TablaReferencia
{
    public string Tref { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Habilitado { get; set; }
    public DateTime LastUpdate { get; set; }
    public List<ItemTablaReferencia> Items { get; set; } = new List<ItemTablaReferencia>();
}
