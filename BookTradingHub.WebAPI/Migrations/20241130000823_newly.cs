using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTradingHub.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class newly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "Users",
                newName: "password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "passwordhash");
        }
    }
}
