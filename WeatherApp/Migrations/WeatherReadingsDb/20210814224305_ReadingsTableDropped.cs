using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.Migrations.WeatherReadingsDb
{
    public partial class ReadingsTableDropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoivodeshipTemps_WeatherReadings_ReadingsId",
                table: "VoivodeshipTemps");

            migrationBuilder.DropTable(
                name: "WeatherReadings");

            migrationBuilder.DropIndex(
                name: "IX_VoivodeshipTemps_ReadingsId",
                table: "VoivodeshipTemps");

            migrationBuilder.DropColumn(
                name: "ReadingsId",
                table: "VoivodeshipTemps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReadingsId",
                table: "VoivodeshipTemps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeatherReadings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReadings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoivodeshipTemps_ReadingsId",
                table: "VoivodeshipTemps",
                column: "ReadingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoivodeshipTemps_WeatherReadings_ReadingsId",
                table: "VoivodeshipTemps",
                column: "ReadingsId",
                principalTable: "WeatherReadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
