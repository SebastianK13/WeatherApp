using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.Migrations.WeatherReadingsDb
{
    public partial class VoivodeshipTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Voivodeship",
                table: "VoivodeshipTemps");

            migrationBuilder.AddColumn<string>(
                name: "VoivodeshipId",
                table: "VoivodeshipTemps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Voivodeships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoivodeshipName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voivodeships", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoivodeshipTemps_VoivodeshipId",
                table: "VoivodeshipTemps",
                column: "VoivodeshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoivodeshipTemps_Voivodeships_VoivodeshipId",
                table: "VoivodeshipTemps",
                column: "VoivodeshipId",
                principalTable: "Voivodeships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoivodeshipTemps_Voivodeships_VoivodeshipId",
                table: "VoivodeshipTemps");

            migrationBuilder.DropTable(
                name: "Voivodeships");

            migrationBuilder.DropIndex(
                name: "IX_VoivodeshipTemps_VoivodeshipId",
                table: "VoivodeshipTemps");

            migrationBuilder.DropColumn(
                name: "VoivodeshipId",
                table: "VoivodeshipTemps");

            migrationBuilder.AddColumn<string>(
                name: "Voivodeship",
                table: "VoivodeshipTemps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
