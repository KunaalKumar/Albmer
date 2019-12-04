using Microsoft.EntityFrameworkCore.Migrations;

namespace Albmer.Migrations
{
    public partial class AlbumUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllMusic",
                table: "Albums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discogs",
                table: "Albums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RateYourMusic",
                table: "Albums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllMusic",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Discogs",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "RateYourMusic",
                table: "Albums");
        }
    }
}
