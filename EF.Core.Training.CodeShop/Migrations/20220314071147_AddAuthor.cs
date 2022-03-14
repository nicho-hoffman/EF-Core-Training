using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF.Core.Training.Migrations
{
    public partial class AddAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Pages",
                table: "Book",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTERGER");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    First = table.Column<string>(type: "TEXT", nullable: true),
                    Last = table.Column<string>(type: "TEXT", nullable: true),
                    Bio = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBookLinks",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: false),
                    BookID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBookLinks", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_AuthorBookLinks_Author_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBookLinks_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookLinks_AuthorID",
                table: "AuthorBookLinks",
                column: "AuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBookLinks");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.AlterColumn<int>(
                name: "Pages",
                table: "Book",
                type: "INTERGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
