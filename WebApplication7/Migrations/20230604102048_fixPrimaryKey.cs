using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations
{
    /// <inheritdoc />
    public partial class fixPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteBooks",
                table: "FavouriteBooks");

            migrationBuilder.AlterColumn<string>(
                name: "TitleAuthor",
                table: "FavouriteBooks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteBooks",
                table: "FavouriteBooks",
                columns: new[] { "UserId", "TitleAuthor" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteBooks",
                table: "FavouriteBooks");

            migrationBuilder.AlterColumn<string>(
                name: "TitleAuthor",
                table: "FavouriteBooks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteBooks",
                table: "FavouriteBooks",
                column: "UserId");
        }
    }
}
