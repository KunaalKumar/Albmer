using Microsoft.EntityFrameworkCore.Migrations;

namespace Albmer.Migrations
{
    public partial class ArtistUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllMusic",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discogs",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficialWebsite",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RateYourMusic",
                table: "Artists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllMusic",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Discogs",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "OfficialWebsite",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "RateYourMusic",
                table: "Artists");
        }
    }
}
