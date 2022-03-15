using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF.Core.Training.Migrations
{
    public partial class Update_api_context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    First = table.Column<string>(type: "TEXT", nullable: true),
                    Last = table.Column<string>(type: "TEXT", nullable: true),
                    Bio = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBookLink",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenreLink", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_BookGenreLink_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenreLink_Genre_GenreID",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookLink_BookID",
                table: "AuthorBookLink",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenreLink");
            migrationBuilder.DropTable(
                name: "BookGenreLink");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBookLink_BookID",
                table: "AuthorBookLink");
        }
    }
}
