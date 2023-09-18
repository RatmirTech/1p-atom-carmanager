using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1p_atom_carmanager.backend.core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TachographInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleSpeed = table.Column<double>(type: "float", nullable: false),
                    DistanceTraveled = table.Column<double>(type: "float", nullable: false),
                    Rpm = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TachographInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleImprovement = table.Column<double>(type: "float", nullable: false),
                    VehicleSpeedWheelBased = table.Column<double>(type: "float", nullable: false),
                    VehicleSpeedFromTachograph = table.Column<double>(type: "float", nullable: false),
                    ClutchSwitch = table.Column<bool>(type: "bit", nullable: false),
                    BrakeSwitch = table.Column<bool>(type: "bit", nullable: false),
                    CruiseControl = table.Column<bool>(type: "bit", nullable: false),
                    PTOStatusMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceleratorPedalPosition = table.Column<double>(type: "float", nullable: false),
                    TotalFuelUsed = table.Column<double>(type: "float", nullable: false),
                    FuelLevel = table.Column<float>(type: "real", nullable: false),
                    EngineSpeed = table.Column<double>(type: "float", nullable: false),
                    GrossAxleWeightRating = table.Column<double>(type: "float", nullable: false),
                    TotalEngineHours = table.Column<double>(type: "float", nullable: false),
                    FMSStandardSoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TachographInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HighResolutionVehicleDistance = table.Column<double>(type: "float", nullable: false),
                    ServiceDistance = table.Column<double>(type: "float", nullable: false),
                    EngineCoolantTemperature = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_TachographInfos_TachographInformationId",
                        column: x => x.TachographInformationId,
                        principalTable: "TachographInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TachographInformationId",
                table: "Cars",
                column: "TachographInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "TachographInfos");
        }
    }
}
