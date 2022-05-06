using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    public partial class MigrationPostgresCarrito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoSesionTable",
                columns: table => new
                {
                    CarritoSesionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSesionTable", x => x.CarritoSesionId);
                });

            migrationBuilder.CreateTable(
                name: "CarritoSesiondetalleTable",
                columns: table => new
                {
                    CarritoSesionDetalleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductoSeleccionado = table.Column<string>(type: "text", nullable: false),
                    CarritoSesionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSesiondetalleTable", x => x.CarritoSesionDetalleId);
                    table.ForeignKey(
                        name: "FK_CarritoSesiondetalleTable_CarritoSesionTable_CarritoSesionId",
                        column: x => x.CarritoSesionId,
                        principalTable: "CarritoSesionTable",
                        principalColumn: "CarritoSesionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoSesiondetalleTable_CarritoSesionId",
                table: "CarritoSesiondetalleTable",
                column: "CarritoSesionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoSesiondetalleTable");

            migrationBuilder.DropTable(
                name: "CarritoSesionTable");
        }
    }
}
