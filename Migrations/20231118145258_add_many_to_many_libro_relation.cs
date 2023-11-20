using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial.Migrations
{
    /// <inheritdoc />
    public partial class add_many_to_many_libro_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libro_Cliente_ClienteId",
                table: "Libro");

            migrationBuilder.DropIndex(
                name: "IX_Libro_ClienteId",
                table: "Libro");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Libro");

            migrationBuilder.CreateTable(
                name: "BibliotecaClientes",
                columns: table => new
                {
                    LibrosId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuariosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaClientes", x => new { x.LibrosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_BibliotecaClientes_Cliente_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecaClientes_Libro_LibrosId",
                        column: x => x.LibrosId,
                        principalTable: "Libro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaClientes_UsuariosId",
                table: "BibliotecaClientes",
                column: "UsuariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibliotecaClientes");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Libro",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libro_ClienteId",
                table: "Libro",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libro_Cliente_ClienteId",
                table: "Libro",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");
        }
    }
}
