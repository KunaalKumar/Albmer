using Microsoft.EntityFrameworkCore.Migrations;

namespace Albmer.Migrations
{
    public partial class ArtistCreditsAndAlbumCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistID",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ArtistID",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "Albums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistID",
                table: "Albums",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistID",
                table: "Albums",
                column: "ArtistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistID",
                table: "Albums",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
