using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTradingHub.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "Books",
                newName: "publisher");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Books",
                newName: "isbn");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "genre");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Books",
                newName: "condition");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "author");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Books",
                newName: "publish_date");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Books",
                newName: "Book_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "publisher",
                table: "Books",
                newName: "Publisher");

            migrationBuilder.RenameColumn(
                name: "isbn",
                table: "Books",
                newName: "ISBN");

            migrationBuilder.RenameColumn(
                name: "genre",
                table: "Books",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "condition",
                table: "Books",
                newName: "Condition");

            migrationBuilder.RenameColumn(
                name: "author",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "publish_date",
                table: "Books",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "Book_Id",
                table: "Books",
                newName: "BookId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
