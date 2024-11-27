using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTradingHub.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    book_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publish_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    averageRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.book_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordhash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    rating_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_Id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    stars = table.Column<int>(type: "int", nullable: false),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.rating_id);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_book_id",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_user_Id",
                        column: x => x.user_Id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_book_id",
                table: "Ratings",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_user_Id",
                table: "Ratings",
                column: "user_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
