using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.Migrations.WeatherReadingsDb
{
    public partial class WeatherReadingsDbReorganized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoivodeshipTemps");

            migrationBuilder.CreateTable(
                name: "Mains",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Temp = table.Column<double>(type: "float", nullable: false),
                    Feels_like = table.Column<double>(type: "float", nullable: false),
                    Temp_min = table.Column<double>(type: "float", nullable: false),
                    Temp_max = table.Column<double>(type: "float", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Winds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    Deg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherReadings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MainId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WindId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoivodeshipId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherReadings_Mains_MainId",
                        column: x => x.MainId,
                        principalTable: "Mains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeatherReadings_Voivodeships_VoivodeshipId",
                        column: x => x.VoivodeshipId,
                        principalTable: "Voivodeships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeatherReadings_Winds_WindId",
                        column: x => x.WindId,
                        principalTable: "Winds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Main = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherReadingsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weathers_WeatherReadings_WeatherReadingsId",
                        column: x => x.WeatherReadingsId,
                        principalTable: "WeatherReadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherReadings_MainId",
                table: "WeatherReadings",
                column: "MainId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherReadings_VoivodeshipId",
                table: "WeatherReadings",
                column: "VoivodeshipId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherReadings_WindId",
                table: "WeatherReadings",
                column: "WindId");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_WeatherReadingsId",
                table: "Weathers",
                column: "WeatherReadingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");

            migrationBuilder.DropTable(
                name: "WeatherReadings");

            migrationBuilder.DropTable(
                name: "Mains");

            migrationBuilder.DropTable(
                name: "Winds");

            migrationBuilder.CreateTable(
                name: "VoivodeshipTemps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReadingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    VoivodeshipId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoivodeshipTemps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoivodeshipTemps_Voivodeships_VoivodeshipId",
                        column: x => x.VoivodeshipId,
                        principalTable: "Voivodeships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoivodeshipTemps_VoivodeshipId",
                table: "VoivodeshipTemps",
                column: "VoivodeshipId");
        }
    }
}
