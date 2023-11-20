using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial.Migrations
{
    /// <inheritdoc />
    public partial class add_store_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    street = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProviderStoreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_Store_ProviderStoreId",
                        column: x => x.ProviderStoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Store_ProviderStoreId",
                table: "Store",
                column: "ProviderStoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
