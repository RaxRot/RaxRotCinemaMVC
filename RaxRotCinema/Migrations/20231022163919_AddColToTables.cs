using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaxRotCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddColToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortBio",
                table: "Producers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortBio",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortBio",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "ShortBio",
                table: "Actors");
        }
    }
}
