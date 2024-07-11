using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bext.Reps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActaConstitucion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaracterTerritorial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelAtencion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaSocialEstado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActoConstitucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroActo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntidadExpide = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiudadExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActaConstitucion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosIdentidad",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosIdentidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modalidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modalidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesAplicacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EsInterno = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesAplicacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terceros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RazonSocial = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TelefonoMovil = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TelefonoFax = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SitioWeb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terceros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terceros_DocumentosIdentidad_TipoIdentificacionId",
                        column: x => x.TipoIdentificacionId,
                        principalTable: "DocumentosIdentidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: true),
                    CodigoInterno = table.Column<int>(type: "int", nullable: false),
                    RolAplicacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Expiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_RolesAplicacion_RolAplicacionId",
                        column: x => x.RolAplicacionId,
                        principalTable: "RolesAplicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPrestador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoNaturalezaJuridica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEntidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoIdentificacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelefonoPrincipal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelefonoAdicional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerceroId = table.Column<int>(type: "int", nullable: false),
                    ActaConstitucionId = table.Column<int>(type: "int", nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ubicacion_Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entidades_ActaConstitucion_ActaConstitucionId",
                        column: x => x.ActaConstitucionId,
                        principalTable: "ActaConstitucion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entidades_DocumentosIdentidad_TipoIdentificacionId",
                        column: x => x.TipoIdentificacionId,
                        principalTable: "DocumentosIdentidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entidades_Terceros_TerceroId",
                        column: x => x.TerceroId,
                        principalTable: "Terceros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoContacto = table.Column<int>(type: "int", nullable: false),
                    TipoIdentificacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DigitoVerificacion = table.Column<short>(type: "smallint", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CorreoInstitucional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoRepresentanteLegal = table.Column<int>(type: "int", nullable: true),
                    Profesion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TarjetaProfesional = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    InformacionOficio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaDocumentoAutorizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    PrimeApellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SegundoNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contactos_DocumentosIdentidad_TipoIdentificacionId",
                        column: x => x.TipoIdentificacionId,
                        principalTable: "DocumentosIdentidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contactos_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosEntidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false)
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
                name: "RegistroModalidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModalidadId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<int>(type: "int", nullable: false),
                    FuncionarioInternoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionarioExternoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroModalidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroModalidades_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroModalidades_Funcionarios_FuncionarioExternoId",
                        column: x => x.FuncionarioExternoId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistroModalidades_Funcionarios_FuncionarioInternoId",
                        column: x => x.FuncionarioInternoId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistroModalidades_Modalidades_ModalidadId",
                        column: x => x.ModalidadId,
                        principalTable: "Modalidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatriculaMercantil = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodigoSede = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DireccionNotificacionJudicial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistroMercantil = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntidadId = table.Column<int>(type: "int", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ubicacion_Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegistroModalidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_RegistroModalidades_RegistroModalidadId",
                        column: x => x.RegistroModalidadId,
                        principalTable: "RegistroModalidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_EntidadId",
                table: "Contactos",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_TipoIdentificacionId",
                table: "Contactos",
                column: "TipoIdentificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_RegistroModalidadId",
                table: "Documentos",
                column: "RegistroModalidadId");

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
                name: "IX_Entidades_TipoIdentificacionId",
                table: "Entidades",
                column: "TipoIdentificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_RolAplicacionId",
                table: "Funcionarios",
                column: "RolAplicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroModalidades_EntidadId",
                table: "RegistroModalidades",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroModalidades_FuncionarioExternoId",
                table: "RegistroModalidades",
                column: "FuncionarioExternoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroModalidades_FuncionarioInternoId",
                table: "RegistroModalidades",
                column: "FuncionarioInternoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroModalidades_ModalidadId",
                table: "RegistroModalidades",
                column: "ModalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_EntidadId",
                table: "Sedes",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Terceros_TipoIdentificacionId",
                table: "Terceros",
                column: "TipoIdentificacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "DocumentosEntidad");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "RegistroModalidades");

            migrationBuilder.DropTable(
                name: "TiposDocumentos");

            migrationBuilder.DropTable(
                name: "Entidades");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Modalidades");

            migrationBuilder.DropTable(
                name: "ActaConstitucion");

            migrationBuilder.DropTable(
                name: "Terceros");

            migrationBuilder.DropTable(
                name: "RolesAplicacion");

            migrationBuilder.DropTable(
                name: "DocumentosIdentidad");
        }
    }
}
