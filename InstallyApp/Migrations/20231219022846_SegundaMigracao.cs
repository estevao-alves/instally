using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstallyApp.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Collections_CollectionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.CreateTable(
                name: "CollectionQuery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionQuery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppQuery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Packages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: true),
                    CollectionQueryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuery_CollectionQuery_CollectionQueryId",
                        column: x => x.CollectionQueryId,
                        principalTable: "CollectionQuery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppQuery_CollectionQueryId",
                table: "AppQuery",
                column: "CollectionQueryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CollectionQuery_CollectionId",
                table: "Users",
                column: "CollectionId",
                principalTable: "CollectionQuery",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CollectionQuery_CollectionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AppQuery");

            migrationBuilder.DropTable(
                name: "CollectionQuery");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionQueriesId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: true),
                    Packages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apps_Collections_CollectionQueriesId",
                        column: x => x.CollectionQueriesId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apps_CollectionQueriesId",
                table: "Apps",
                column: "CollectionQueriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Collections_CollectionId",
                table: "Users",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }
    }
}
