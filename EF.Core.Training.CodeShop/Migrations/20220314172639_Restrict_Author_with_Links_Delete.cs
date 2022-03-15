using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF.Core.Training.Migrations
{
    public partial class Restrict_Author_with_Links_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookLink_Author_AuthorID",
                table: "AuthorBookLink");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookLink_Author_AuthorID",
                table: "AuthorBookLink",
                column: "AuthorID",
                principalTable: "Author",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookLink_Author_AuthorID",
                table: "AuthorBookLink");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookLink_Author_AuthorID",
                table: "AuthorBookLink",
                column: "AuthorID",
                principalTable: "Author",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
