using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTradingHub.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_book_Id1",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_book_Id1",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "book_Id1",
                table: "Ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "book_Id1",
                table: "Ratings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_book_Id1",
                table: "Ratings",
                column: "book_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_book_Id1",
                table: "Ratings",
                column: "book_Id1",
                principalTable: "Books",
                principalColumn: "book_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
