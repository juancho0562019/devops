using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bext.Reps.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<RepsDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class RepsDbContextInitialiser
{
    private readonly ILogger<RepsDbContextInitialiser> _logger;
    private readonly RepsDbContext _context;

    public RepsDbContextInitialiser(ILogger<RepsDbContextInitialiser> logger, RepsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {


        // Default data
        // Seed, if necessary
        if (!await _context.TiposPersona.AnyAsync())
        {
            await _context.TiposPersona.AddRangeAsync(new List<TipoPersona>
            {

                new TipoPersona
                {
                    Id = "PJ",
                    Nombre = "Persona Juridica"
                },
                new TipoPersona
                {
                    Id = "PN",
                    Nombre = "Persona Natural"
                }
            });

            await _context.SaveChangesAsync();
        }
        if (!await _context.TiposIdentidad.AnyAsync())
        {
            await _context.TiposIdentidad.AddRangeAsync(new List<TipoIdentidad> 
            {
                
                new TipoIdentidad 
                {
                    Id = "CC",
                    Nombre = "Cedula de ciudadania"
                },
                new TipoIdentidad
                {
                    Id = "NI",
                    Nombre = "Nit"
                },
                new TipoIdentidad
                {
                    Id = "PA",
                    Nombre = "Pasaporte"
                }
            });

            await _context.SaveChangesAsync();
        }
        if (!await _context.TiposNaturaleza.AnyAsync())
        {
            var actoAdministrativo = new DocumentoConstitucion
            {
                Id = "03",
                Nombre = "Acto Administrativo"
            };
            await _context.TiposNaturaleza.AddRangeAsync(new List<TipoNaturaleza>
            {
                new TipoNaturaleza
                {
                    Id = "01",
                    Nombre = "Naturaleza Juridica Publica",
                    SubTipoNaturalezas = new List<SubTipoNaturaleza>()
                    {
                        new SubTipoNaturaleza
                        {
                            Id = "23",
                            Nombre = "Entidades de Derecho Público",
                            DocumentosConstitucion = new List<DocumentoConstitucion>
                            {
                                actoAdministrativo
                            }
                        }
                    },
                    CaracterTerritorial = new List<CaracterTerritorial>
                    {
                        new CaracterTerritorial
                        {
                            Id = "01",
                            Nombre = "Nacional",
                            Descripcion = "Entidades que tienen jurisdicción y operan en todo el territorio nacional"
                        },
                        new CaracterTerritorial
                        {
                            Id = "02",
                            Nombre = "Departamental",
                            Descripcion = "Entidades cuya jurisdicción se limita a un departamento específico dentro del país"
                        },
                        new CaracterTerritorial
                        {
                            Id = "03",
                            Nombre = "Municipal",
                            Descripcion = "Entidades que operan dentro de un municipio específico"
                        },
                        new CaracterTerritorial
                        {
                            Id = "04",
                            Nombre = "Distrital",
                            Descripcion = "Entidades que tienen jurisdicción en distritos especiales, que pueden tener características administrativas y fiscales particulares"
                        },
                        new CaracterTerritorial
                        {
                            Id = "05",
                            Nombre = "Regional",
                            Descripcion = "Entidades cuya jurisdicción abarca una región que puede incluir varios departamentos o municipios"
                        }
                    }
                },
                new TipoNaturaleza
                {
                    Id = "02",
                    Nombre = "Naturaleza Juridica Privada",
                     SubTipoNaturalezas = new List<SubTipoNaturaleza>()
                    {
                        new SubTipoNaturaleza
                        {
                            Id = "21",
                            Nombre = "Con Ánimo de Lucro",
                            DocumentosConstitucion = new List<DocumentoConstitucion>
                            {
                                new DocumentoConstitucion
                                {
                                    Id = "01",
                                    Nombre = "Matricula Mercantil"
                                },
                            }
                        },
                        new SubTipoNaturaleza
                        {
                            Id = "22",
                            Nombre = "Sin Ánimo de Lucro",
                             DocumentosConstitucion = new List<DocumentoConstitucion>
                            {
                                new DocumentoConstitucion
                                {
                                    Id = "02",
                                    Nombre = "Resolucion"
                                },
                                actoAdministrativo
                            }
                        }
                    }
                },
                new TipoNaturaleza 
                {
                    Id = "03",
                    Nombre = "Naturaleza Juridica Mixta"
                }
            });

            await _context.SaveChangesAsync();
        }
        if (!await _context.ClasesPrestador.AnyAsync())
        {
            await _context.ClasesPrestador.AddRangeAsync(new List<ClasePrestador>
            {

                new ClasePrestador
                {
                    Id = "01",
                    Nombre = "Instituciones Prestadoras de Servicios de Salud (IPS)"
                },
                new ClasePrestador
                {
                    Id = "02",
                    Nombre = "Profesionales Independientes"
                },
                new ClasePrestador
                {
                    Id = "04",
                    Nombre = "Entidad con Objeto Social Diferente"
                },
                new ClasePrestador
                {
                    Id ="06",
                    Nombre = "Transporte Especial de Paciente"
                }
            });

            await _context.SaveChangesAsync();
        }


        if (!await _context.DocumentosConstitucion.AnyAsync())
        {
            await _context.DocumentosConstitucion.AddRangeAsync(new List<DocumentoConstitucion>
            {

                new DocumentoConstitucion
                {
                    Id = "01",
                    Nombre = "Instituciones Prestadoras de Servicios de Salud (IPS)"
                },
                new DocumentoConstitucion
                {
                    Id = "02",
                    Nombre = "Profesionales Independientes"
                },
            });

            await _context.SaveChangesAsync();
        }
        if (!await _context.TiposVinculacion.AnyAsync())
        {
            await _context.TiposVinculacion.AddRangeAsync(new List<TipoVinculacion>
            {

                new TipoVinculacion
                {
                    Id = "01",
                    Nombre = "Nombramiento"
                },
                new TipoVinculacion
                {
                    Id = "02",
                    Nombre = "Elección"
                },
                new TipoVinculacion
                {
                    Id = "03",
                    Nombre = "Contratación"
                },
            });

            await _context.SaveChangesAsync();
        }
        if (!await _context.NivelesAtencion.AnyAsync())
        {
            await _context.NivelesAtencion.AddRangeAsync(new List<NivelAtencion>
            {

                new NivelAtencion
                {
                    Nombre = "Nivel 1",
                    Nivel = 1
                },
                new NivelAtencion
                {
                    Nombre = "Nivel 2",
                    Nivel = 2
                },
                new NivelAtencion
                {
                    Nombre = "Nivel 3",
                    Nivel = 3
                },
                new NivelAtencion
                {
                    Nombre = "Nivel 4",
                    Nivel = 4

                },
            });

            await _context.SaveChangesAsync();
        }
        if (!await _context.TiposDocumentos.AnyAsync())
        {
            await _context.TiposDocumentos.AddRangeAsync(new List<TipoDocumento>
            {

                new TipoDocumento
                {
                    Nombre = "Documento Identificación",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Titulos de educación superior de pregado o posgrado segun aplique",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Tarjeta profesional",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Tarjeta Inspacción acreditado por la ONAC",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Certificación expedida por un profesional competente",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Certificado de existencia",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Licencia de construcción",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Permisos propiedad horizontal",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Certificación de suficiencia",
                    Tipo = TipoDocumentoPrestador.Prestador
                },
                new TipoDocumento
                {
                    Nombre = "Certificación Sede",
                    Tipo = TipoDocumentoPrestador.Sede
                },
                new TipoDocumento
                {
                    Nombre = "Permisos Sede",
                    Tipo = TipoDocumentoPrestador.Sede
                },
                new TipoDocumento
                {
                    Nombre = "Registro de Procesos Prioritarios",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Registro de Formación y Capacitación del Talento Humano",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Certificados de Conformidad",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Protocolo de Atención",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Plan de Mantenimiento de Equipos Biomédicos",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Soportes Documentales para Modalidad Extramural",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Registro de Procesos Prioritarios",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Registro de Formación y Capacitacón del Talento Humano",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
                new TipoDocumento
                {
                    Nombre = "Certificados de Conformidad",
                    Tipo = TipoDocumentoPrestador.Servicio
                },
            });

            await _context.SaveChangesAsync();
        }

        if (!await _context.GruposServicio.AnyAsync())
        {
            var modalidadIntramural = new Modalidad 
            {
                Nombre = "IntraMural"
            };
            var modalidadExtramural = new Modalidad
            {
                Nombre = "Extramural"
            };

            var modalidadTelemedicina = new Modalidad
            {
                Nombre = "Telemedicina"
            };

            await _context.GruposServicio.AddRangeAsync(new List<GrupoServicio>
            {
                new GrupoServicio
                {
                    Nombre = "Consulta Externa",
                    Modalidad = modalidadIntramural,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Medicina General" },
                        new Servicio { Nombre = "Pediatría" },
                        new Servicio { Nombre = "Ginecología y Obstetricia" },
                        new Servicio { Nombre = "Cardiología" },
                        new Servicio { Nombre = "Dermatología" },
                        new Servicio { Nombre = "Endocrinología" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Urgencias",
                    Modalidad = modalidadIntramural,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Atención de Urgencias" },
                        new Servicio { Nombre = "Reanimación y Cuidados Críticos" },
                        new Servicio { Nombre = "Triage" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Hospitalización",
                    Modalidad = modalidadIntramural,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Hospitalización en Medicina Interna" },
                        new Servicio { Nombre = "Hospitalización en Cirugía General" },
                        new Servicio { Nombre = "Hospitalización en Pediatría" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Unidad Móvil",
                    Modalidad = modalidadExtramural,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Consulta Médica" },
                        new Servicio { Nombre = "Vacunación" },
                        new Servicio { Nombre = "Control Prenatal" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Domiciliario",
                    Modalidad = modalidadExtramural,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Atención Médica Domiciliaria" },
                        new Servicio { Nombre = "Rehabilitación Domiciliaria" },
                        new Servicio { Nombre = "Enfermería Domiciliaria" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Jornada de Salud",
                    Modalidad = modalidadExtramural,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Campañas de Vacunación" },
                        new Servicio { Nombre = "Charlas de Prevención y Promoción" },
                        new Servicio { Nombre = "Consultas Médicas Grupales" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Prestador Referencia",
                    Modalidad = modalidadTelemedicina,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Telemedicina Interactiva" },
                        new Servicio { Nombre = "Telemedicina No Interactiva" },
                        new Servicio { Nombre = "Telexperticia" },
                        new Servicio { Nombre = "Telemonitoreo" }
                    }
                },
                new GrupoServicio
                {
                    Nombre = "Prestador Remisor",
                    Modalidad = modalidadTelemedicina,
                    Servicios = new List<Servicio>
                    {
                        new Servicio { Nombre = "Telemedicina Interactiva" },
                        new Servicio { Nombre = "Telemedicina No Interactiva" }
                    }
                },
            });

            await _context.SaveChangesAsync();
        }

        if (!await _context.EstandarPorServicios.AnyAsync())
        {
            var criterios = new List<Criterio>
                    {
                        new Criterio
                        {
                            Nombre = "Cuenta con: Técnico profesional o tecnólogo en imágenes diagnósticas, para la operación de equipos"
                        },
                        new Criterio
                        {
                            Nombre = "Disponibilidad de: Médico especializado en radiología e imágenes diagnósticas o aquellos médicos especialistas"
                        },
                        new Criterio
                        {
                            Nombre = "Tecnólogo en radiología e imágenes diagnósticas para la operación de equipos y adquisición de imágenes"
                        },
                        new Criterio
                        {
                            Nombre = "Médico especializado en radiología e imágenes diagnósticas"
                        },
                        new Criterio
                        {
                            Nombre = "Médicos especialistas quienes en su pensum o formación académica hayan adquirido los conocimientos del manejo"
                        }
                    };
            await _context.Estandares.AddRangeAsync(new List<Estandar>
            {

                new Estandar
                {
                    Nombre = "Estandar de Talento Humano",
                    Criterios = criterios
                },
                new Estandar
                {
                    Nombre = "Estandar de Infraestructura",
                    
                },
                new Estandar
                {
                    Nombre = "Estandar de Dotacion",
                           Criterios = criterios
                },
            });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Especificidades.AnyAsync())
        {
            await _context.Especificidades.AddRangeAsync(new List<Especificidad>
                {
                    new Especificidad
                    {
                        Nombre = "Especificidad1"
                    },
                    new Especificidad
                    {
                        Nombre = "Especificidad2"
                    },
                    new Especificidad
                    {
                        Nombre = "Especificidad3"
                    },
                    new Especificidad
                    {
                        Nombre = "Especificidad4"
                    },
                });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Complejidades.AnyAsync())
        {
            await _context.Complejidades.AddRangeAsync(new List<Complejidad>
                {
                    new Complejidad
                    {
                        Nivel = "Bajo"
                    },
                    new Complejidad
                    {
                        Nivel = "Medio"
                    },
                    new Complejidad
                    {
                        Nivel = "Alto"
                    }
                });
            await _context.SaveChangesAsync();
        }
    }
}
