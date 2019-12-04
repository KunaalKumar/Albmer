using Microsoft.EntityFrameworkCore.Migrations;

namespace Albmer.Migrations
{
    public partial class ArtistListForAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    BeginYear = table.Column<string>(nullable: true),
                    EndYear = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BandID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    TrackCount = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    ArtistID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Albums_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistID",
                table: "Albums",
                column: "ArtistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
