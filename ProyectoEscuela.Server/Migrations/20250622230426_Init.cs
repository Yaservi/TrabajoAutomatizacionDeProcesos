using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoEscuela.Server.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKAlumno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Maestros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKMaestro", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreMateria = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(850)", maxLength: 850, nullable: false),
                    MaestroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKMateria", x => x.id);
                    table.ForeignKey(
                        name: "FK_Materias_Maestros_MaestroId",
                        column: x => x.MaestroId,
                        principalTable: "Maestros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoMateria",
                columns: table => new
                {
                    Alumnoid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Materiaid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoMateria", x => new { x.Alumnoid, x.Materiaid });
                    table.ForeignKey(
                        name: "FK_AlumnoMateria_Alumnos_Alumnoid",
                        column: x => x.Alumnoid,
                        principalTable: "Alumnos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnoMateria_Materias_Materiaid",
                        column: x => x.Materiaid,
                        principalTable: "Materias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaAsistencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlumnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKAsistencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Asistencias_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencias_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Participacion = table.Column<double>(type: "float", nullable: false),
                    PrimerParcial = table.Column<double>(type: "float", nullable: false),
                    SegundoParcial = table.Column<double>(type: "float", nullable: false),
                    ExamenFinal = table.Column<double>(type: "float", nullable: false),
                    TrabajoInvestigacion = table.Column<double>(type: "float", nullable: false),
                    TrabajoFinal = table.Column<double>(type: "float", nullable: false),
                    Nota = table.Column<double>(type: "float", nullable: false),
                    IdAlumno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMateria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PKCalificacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Alumnos_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "Alumnos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoMateria_Materiaid",
                table: "AlumnoMateria",
                column: "Materiaid");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_AlumnoId",
                table: "Asistencias",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_MateriaId",
                table: "Asistencias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdAlumno",
                table: "Calificaciones",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdMateria",
                table: "Calificaciones",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_MaestroId",
                table: "Materias",
                column: "MaestroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoMateria");

            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Maestros");
        }
    }
}
