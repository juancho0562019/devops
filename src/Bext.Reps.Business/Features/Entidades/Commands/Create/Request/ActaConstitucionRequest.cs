namespace Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
public record ActaConstitucionRequest(
    
    string CaracterTerritorial,
    
    int? NivelAtencion,
    
    string? EmpresaSocialEstado,
    
    string ActoConstitucion,
    
    string NumeroActo,
    
    DateTime FechaActo,
    
    string EntidadExpide,
    
    string CiudadExpedicion);
