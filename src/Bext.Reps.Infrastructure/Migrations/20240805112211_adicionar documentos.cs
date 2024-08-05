using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bext.Reps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionardocumentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "DocumentosServicios");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "DocumentosEntidad");

            migrationBuilder.AddColumn<int>(
                name: "EstadoDocumento",
                table: "DocumentosServicios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoDocumento",
                table: "DocumentosEntidad",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoDocumento",
                table: "DocumentosServicios");

            migrationBuilder.DropColumn(
                name: "EstadoDocumento",
                table: "DocumentosEntidad");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "DocumentosServicios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "DocumentosEntidad",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
