using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.Migrations.WeatherReadingsDb
{
    public partial class ZipCodeColumnDropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Voivodeships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Voivodeships",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
