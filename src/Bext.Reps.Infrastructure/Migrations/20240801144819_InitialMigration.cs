using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bext.Reps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClasesPrestador",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasesPrestador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complejidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complejidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especificidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especificidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estandares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estandares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GruposServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    GrupoPadreId = table.Column<int>(type: "int", nullable: true),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GruposServicio_GruposServicio_GrupoPadreId",
                        column: x => x.GrupoPadreId,
                        principalTable: "GruposServicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NivelesAtencion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesAtencion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoSolicitud = table.Column<int>(type: "int", nullable: false),
                    EstadoSolicitud = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposIdentidad",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposNaturaleza",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposNaturaleza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPersona",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    RequiereRepresentante = table.Column<bool>(type: "bit", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPersona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposVinculacion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVinculacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Criterios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EstandarId = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterios_Estandares_EstandarId",
                        column: x => x.EstandarId,
                        principalTable: "Estandares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    GrupoServicioId = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicios_GruposServicio_GrupoServicioId",
                        column: x => x.GrupoServicioId,
                        principalTable: "GruposServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoSolicitud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    EstadoSolicitud = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoSolicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientoSolicitud_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaracteresTerritoriales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoNaturalezaId = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteresTerritoriales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaracteresTerritoriales_TiposNaturaleza_TipoNaturalezaId",
                        column: x => x.TipoNaturalezaId,
                        principalTable: "TiposNaturaleza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubTiposNaturaleza",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TipoNaturalezaId = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTiposNaturaleza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTiposNaturaleza_TiposNaturaleza_TipoNaturalezaId",
                        column: x => x.TipoNaturalezaId,
                        principalTable: "TiposNaturaleza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Terceros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPersonaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SitioWeb = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TelefonoFax = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TelefonoMovil = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PrimerNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SegundoNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terceros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terceros_TiposPersona_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalTable: "TiposPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactosEntidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoVinculacionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PrimerNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SegundoNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactosEntidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactosEntidad_TiposVinculacion_TipoVinculacionId",
                        column: x => x.TipoVinculacionId,
                        principalTable: "TiposVinculacion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EstandarPorServicios",
                columns: table => new
                {
                    EstandarId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstandarPorServicios", x => new { x.ServicioId, x.EstandarId });
                    table.ForeignKey(
                        name: "FK_EstandarPorServicios_Estandares_EstandarId",
                        column: x => x.EstandarId,
                        principalTable: "Estandares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstandarPorServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosConstitucion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SubTipoNaturalezaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosConstitucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosConstitucion_SubTiposNaturaleza_SubTipoNaturalezaId",
                        column: x => x.SubTipoNaturalezaId,
                        principalTable: "SubTiposNaturaleza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActasConstitucion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaracterTerritorialId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NivelAtencionId = table.Column<int>(type: "int", nullable: true),
                    EmpresaSocialEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActoConstitucionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NumeroActo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntidadExpide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiudadExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActasConstitucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActasConstitucion_CaracteresTerritoriales_CaracterTerritorialId",
                        column: x => x.CaracterTerritorialId,
                        principalTable: "CaracteresTerritoriales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActasConstitucion_DocumentosConstitucion_ActoConstitucionId",
                        column: x => x.ActoConstitucionId,
                        principalTable: "DocumentosConstitucion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActasConstitucion_NivelesAtencion_NivelAtencionId",
                        column: x => x.NivelAtencionId,
                        principalTable: "NivelesAtencion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPersonaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoPrestadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoNaturalezaId = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    SubTipoNaturaleza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActaConstitucionId = table.Column<int>(type: "int", nullable: true),
                    TerceroId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SitioWeb = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TelefonoFax = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TelefonoMovil = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entidades_ActasConstitucion_ActaConstitucionId",
                        column: x => x.ActaConstitucionId,
                        principalTable: "ActasConstitucion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entidades_ClasesPrestador_TipoPrestadorId",
                        column: x => x.TipoPrestadorId,
                        principalTable: "ClasesPrestador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entidades_Terceros_TerceroId",
                        column: x => x.TerceroId,
                        principalTable: "Terceros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entidades_TiposNaturaleza_TipoNaturalezaId",
                        column: x => x.TipoNaturalezaId,
                        principalTable: "TiposNaturaleza",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entidades_TiposPersona_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalTable: "TiposPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosEntidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosEntidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosEntidad_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentosEntidad_TiposDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TiposDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeriodoRepresentacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoRepresentacion = table.Column<int>(type: "int", nullable: false),
                    ContactoId = table.Column<int>(type: "int", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodoRepresentacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodoRepresentacion_ContactosEntidad_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "ContactosEntidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeriodoRepresentacion_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreResponsable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentroPoblado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barrio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SitioWeb = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TelefonoFax = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TelefonoMovil = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ubicacion_Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sedes_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicioInscritoSede",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    GrupoServicioId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    ComplejidadServicioId = table.Column<int>(type: "int", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: true),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioInscritoSede", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicioInscritoSede_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicioInscritoSede_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspecificidadPorServiciosInscritosSede",
                columns: table => new
                {
                    EspecificidadId = table.Column<int>(type: "int", nullable: false),
                    ServicioInscritoSedeId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: true),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecificidadPorServiciosInscritosSede", x => new { x.ServicioInscritoSedeId, x.EspecificidadId });
                    table.ForeignKey(
                        name: "FK_EspecificidadPorServiciosInscritosSede_Especificidades_EspecificidadId",
                        column: x => x.EspecificidadId,
                        principalTable: "Especificidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EspecificidadPorServiciosInscritosSede_ServicioInscritoSede_ServicioInscritoSedeId",
                        column: x => x.ServicioInscritoSedeId,
                        principalTable: "ServicioInscritoSede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EspecificidadPorServiciosInscritosSede_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEvaluacion = table.Column<int>(type: "int", nullable: false),
                    ServicioInscritoSedeId = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionServicio_ServicioInscritoSede_ServicioInscritoSedeId",
                        column: x => x.ServicioInscritoSedeId,
                        principalTable: "ServicioInscritoSede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleEvaluacionServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstandarId = table.Column<int>(type: "int", nullable: false),
                    CriterioId = table.Column<int>(type: "int", nullable: false),
                    Cumple = table.Column<bool>(type: "bit", nullable: false),
                    EvaluacionServicioId = table.Column<int>(type: "int", nullable: true),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleEvaluacionServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleEvaluacionServicio_Criterios_CriterioId",
                        column: x => x.CriterioId,
                        principalTable: "Criterios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleEvaluacionServicio_Estandares_EstandarId",
                        column: x => x.EstandarId,
                        principalTable: "Estandares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleEvaluacionServicio_EvaluacionServicio_EvaluacionServicioId",
                        column: x => x.EvaluacionServicioId,
                        principalTable: "EvaluacionServicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActasConstitucion_ActoConstitucionId",
                table: "ActasConstitucion",
                column: "ActoConstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActasConstitucion_CaracterTerritorialId",
                table: "ActasConstitucion",
                column: "CaracterTerritorialId");

            migrationBuilder.CreateIndex(
                name: "IX_ActasConstitucion_NivelAtencionId",
                table: "ActasConstitucion",
                column: "NivelAtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_CaracteresTerritoriales_TipoNaturalezaId",
                table: "CaracteresTerritoriales",
                column: "TipoNaturalezaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactosEntidad_TipoVinculacionId",
                table: "ContactosEntidad",
                column: "TipoVinculacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterios_EstandarId",
                table: "Criterios",
                column: "EstandarId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEvaluacionServicio_CriterioId",
                table: "DetalleEvaluacionServicio",
                column: "CriterioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEvaluacionServicio_EstandarId",
                table: "DetalleEvaluacionServicio",
                column: "EstandarId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEvaluacionServicio_EvaluacionServicioId",
                table: "DetalleEvaluacionServicio",
                column: "EvaluacionServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosConstitucion_SubTipoNaturalezaId",
                table: "DocumentosConstitucion",
                column: "SubTipoNaturalezaId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosEntidad_EntidadId",
                table: "DocumentosEntidad",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosEntidad_TipoDocumentoId",
                table: "DocumentosEntidad",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_ActaConstitucionId",
                table: "Entidades",
                column: "ActaConstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TerceroId",
                table: "Entidades",
                column: "TerceroId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TipoNaturalezaId",
                table: "Entidades",
                column: "TipoNaturalezaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TipoPersonaId",
                table: "Entidades",
                column: "TipoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidades_TipoPrestadorId",
                table: "Entidades",
                column: "TipoPrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecificidadPorServiciosInscritosSede_EspecificidadId",
                table: "EspecificidadPorServiciosInscritosSede",
                column: "EspecificidadId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecificidadPorServiciosInscritosSede_ServicioId",
                table: "EspecificidadPorServiciosInscritosSede",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_EstandarPorServicios_EstandarId",
                table: "EstandarPorServicios",
                column: "EstandarId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionServicio_ServicioInscritoSedeId",
                table: "EvaluacionServicio",
                column: "ServicioInscritoSedeId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposServicio_GrupoPadreId",
                table: "GruposServicio",
                column: "GrupoPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoSolicitud_SolicitudId",
                table: "MovimientoSolicitud",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodoRepresentacion_ContactoId",
                table: "PeriodoRepresentacion",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodoRepresentacion_EntidadId",
                table: "PeriodoRepresentacion",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_EntidadId",
                table: "Sedes",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioInscritoSede_EntidadId",
                table: "ServicioInscritoSede",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioInscritoSede_SolicitudId",
                table: "ServicioInscritoSede",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_GrupoServicioId",
                table: "Servicios",
                column: "GrupoServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTiposNaturaleza_TipoNaturalezaId",
                table: "SubTiposNaturaleza",
                column: "TipoNaturalezaId");

            migrationBuilder.CreateIndex(
                name: "IX_Terceros_TipoPersonaId",
                table: "Terceros",
                column: "TipoPersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complejidades");

            migrationBuilder.DropTable(
                name: "DetalleEvaluacionServicio");

            migrationBuilder.DropTable(
                name: "DocumentosEntidad");

            migrationBuilder.DropTable(
                name: "EspecificidadPorServiciosInscritosSede");

            migrationBuilder.DropTable(
                name: "EstandarPorServicios");

            migrationBuilder.DropTable(
                name: "MovimientoSolicitud");

            migrationBuilder.DropTable(
                name: "PeriodoRepresentacion");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "TiposIdentidad");

            migrationBuilder.DropTable(
                name: "Criterios");

            migrationBuilder.DropTable(
                name: "EvaluacionServicio");

            migrationBuilder.DropTable(
                name: "TiposDocumentos");

            migrationBuilder.DropTable(
                name: "Especificidades");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "ContactosEntidad");

            migrationBuilder.DropTable(
                name: "Estandares");

            migrationBuilder.DropTable(
                name: "ServicioInscritoSede");

            migrationBuilder.DropTable(
                name: "GruposServicio");

            migrationBuilder.DropTable(
                name: "TiposVinculacion");

            migrationBuilder.DropTable(
                name: "Entidades");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "ActasConstitucion");

            migrationBuilder.DropTable(
                name: "ClasesPrestador");

            migrationBuilder.DropTable(
                name: "Terceros");

            migrationBuilder.DropTable(
                name: "CaracteresTerritoriales");

            migrationBuilder.DropTable(
                name: "DocumentosConstitucion");

            migrationBuilder.DropTable(
                name: "NivelesAtencion");

            migrationBuilder.DropTable(
                name: "TiposPersona");

            migrationBuilder.DropTable(
                name: "SubTiposNaturaleza");

            migrationBuilder.DropTable(
                name: "TiposNaturaleza");
        }
    }
}
