using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bext.Reps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacionradicacionserviciofranjahoraria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposServicio_GruposServicio_GrupoPadreId",
                table: "GruposServicio");

            migrationBuilder.DropIndex(
                name: "IX_GruposServicio_GrupoPadreId",
                table: "GruposServicio");

            migrationBuilder.DropColumn(
                name: "GrupoPadreId",
                table: "GruposServicio");

            migrationBuilder.AddColumn<int>(
                name: "Modalidad",
                table: "GruposServicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModalidadCodigo",
                table: "GruposServicio",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CapacidadInstalada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioInscritoSedeId = table.Column<int>(type: "int", nullable: false),
                    TipoRecurso = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacidadInstalada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapacidadInstalada_ServicioInscritoSede_ServicioInscritoSedeId",
                        column: x => x.ServicioInscritoSedeId,
                        principalTable: "ServicioInscritoSede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FranjasHoraria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioInscritoSedeId = table.Column<int>(type: "int", nullable: false),
                    HoraApertura = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraCierre = table.Column<TimeSpan>(type: "time", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranjasHoraria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FranjasHoraria_ServicioInscritoSede_ServicioInscritoSedeId",
                        column: x => x.ServicioInscritoSedeId,
                        principalTable: "ServicioInscritoSede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiasAtencion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FranjaHorariaId = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltimaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimaModificacionPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoRegistro = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasAtencion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiasAtencion_FranjasHoraria_FranjaHorariaId",
                        column: x => x.FranjaHorariaId,
                        principalTable: "FranjasHoraria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapacidadesInstaladas_ServicioInscritoSede_TipoRecurso_Activo",
                table: "CapacidadInstalada",
                columns: new[] { "ServicioInscritoSedeId", "TipoRecurso", "Activo" });

            migrationBuilder.CreateIndex(
                name: "IX_DiasAtencion_FranjaHorariaId",
                table: "DiasAtencion",
                column: "FranjaHorariaId");

            migrationBuilder.CreateIndex(
                name: "IX_FranjasHoraria_ServicioInscritoSedeId",
                table: "FranjasHoraria",
                column: "ServicioInscritoSedeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapacidadInstalada");

            migrationBuilder.DropTable(
                name: "DiasAtencion");

            migrationBuilder.DropTable(
                name: "FranjasHoraria");

            migrationBuilder.DropColumn(
                name: "Modalidad",
                table: "GruposServicio");

            migrationBuilder.DropColumn(
                name: "ModalidadCodigo",
                table: "GruposServicio");

            migrationBuilder.AddColumn<int>(
                name: "GrupoPadreId",
                table: "GruposServicio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GruposServicio_GrupoPadreId",
                table: "GruposServicio",
                column: "GrupoPadreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposServicio_GruposServicio_GrupoPadreId",
                table: "GruposServicio",
                column: "GrupoPadreId",
                principalTable: "GruposServicio",
                principalColumn: "Id");
        }
    }
}
