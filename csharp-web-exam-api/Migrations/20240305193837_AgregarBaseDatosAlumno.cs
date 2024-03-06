using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_web_exam_api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBaseDatosAlumno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "test");

            migrationBuilder.CreateTable(
                name: "GENERO",
                schema: "test",
                columns: table => new
                {
                    GENERO_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GENERO_DESC = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENERO", x => x.GENERO_ID);
                });

            migrationBuilder.CreateTable(
                name: "ALUMNO",
                schema: "test",
                columns: table => new
                {
                    ALUMNO_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PATERNO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MATERNO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GENERO_ID = table.Column<int>(type: "int", nullable: false),
                    CORREO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUMNO", x => x.ALUMNO_ID);
                    table.ForeignKey(
                        name: "FK_ALUMNO_GENERO_GENERO_ID",
                        column: x => x.GENERO_ID,
                        principalSchema: "test",
                        principalTable: "GENERO",
                        principalColumn: "GENERO_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_ALUMNO_ID",
                schema: "test",
                table: "ALUMNO",
                column: "ALUMNO_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ALUMNO_GENERO_ID",
                schema: "test",
                table: "ALUMNO",
                column: "GENERO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GENERO_GENERO_ID",
                schema: "test",
                table: "GENERO",
                column: "GENERO_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALUMNO",
                schema: "test");

            migrationBuilder.DropTable(
                name: "GENERO",
                schema: "test");
        }
    }
}
