using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Actividad5LengProg3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CantidadCreditos = table.Column<int>(type: "int", nullable: false),
                    CantidadMaterias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Recintos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recintos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    RecintoId = table.Column<int>(type: "int", nullable: false),
                    CorreoInstitucional = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    TurnoEstudiante = table.Column<int>(type: "int", nullable: false),
                    Becado = table.Column<bool>(type: "bit", nullable: false),
                    PorcentajeBeca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Matricula);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Recintos_RecintoId",
                        column: x => x.RecintoId,
                        principalTable: "Recintos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Codigo", "CantidadCreditos", "CantidadMaterias", "Nombre" },
                values: new object[,]
                {
                    { 1, 150, 40, "Administración de Empresas" },
                    { 2, 145, 38, "Contabilidad" },
                    { 3, 160, 42, "Derecho" },
                    { 4, 155, 41, "Psicología Clínica" },
                    { 5, 148, 39, "Orientación Escolar" },
                    { 6, 150, 40, "Administración y Supervisión Escolar" },
                    { 7, 165, 44, "Enfermería" },
                    { 8, 180, 48, "Odontología" },
                    { 9, 170, 46, "Ingeniería" },
                    { 10, 152, 40, "Ciencias Naturales" }
                });

            migrationBuilder.InsertData(
                table: "Recintos",
                columns: new[] { "Codigo", "Direccion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Santo Domingo Oeste", "Herrera" },
                    { 2, "Santo Domingo Este", "Metropolitano" },
                    { 3, "Barahona", "Barahona" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_CarreraId",
                table: "Estudiantes",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_CorreoInstitucional",
                table: "Estudiantes",
                column: "CorreoInstitucional",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_RecintoId",
                table: "Estudiantes",
                column: "RecintoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Recintos");
        }
    }
}
