using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bext.Reps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modalidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModalidadCodigo",
                table: "GruposServicio");

            migrationBuilder.RenameColumn(
                name: "Modalidad",
                table: "GruposServicio",
                newName: "ModalidadId");

            migrationBuilder.CreateTable(
                name: "DocumentosServicios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_DocumentosServicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosServicios_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentosServicios_TiposDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TiposDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modalidad",
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
                    table.PrimaryKey("PK_Modalidad", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_EntidadId",
                table: "Solicitudes",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposServicio_ModalidadId",
                table: "GruposServicio",
                column: "ModalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosServicios_SolicitudId",
                table: "DocumentosServicios",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosServicios_TipoDocumentoId",
                table: "DocumentosServicios",
                column: "TipoDocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposServicio_Modalidad_ModalidadId",
                table: "GruposServicio",
                column: "ModalidadId",
                principalTable: "Modalidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Entidades_EntidadId",
                table: "Solicitudes",
                column: "EntidadId",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposServicio_Modalidad_ModalidadId",
                table: "GruposServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Entidades_EntidadId",
                table: "Solicitudes");

            migrationBuilder.DropTable(
                name: "DocumentosServicios");

            migrationBuilder.DropTable(
                name: "Modalidad");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_EntidadId",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_GruposServicio_ModalidadId",
                table: "GruposServicio");

            migrationBuilder.RenameColumn(
                name: "ModalidadId",
                table: "GruposServicio",
                newName: "Modalidad");

            migrationBuilder.AddColumn<string>(
                name: "ModalidadCodigo",
                table: "GruposServicio",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }
    }
}
