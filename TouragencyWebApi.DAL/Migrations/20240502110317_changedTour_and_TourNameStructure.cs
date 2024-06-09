using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TouragencyWebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedTour_and_TourNameStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHaveNightRides",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "NightRidesCount",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "Tours");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "TourNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsHaveNightRides",
                table: "TourNames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "NightRidesCount",
                table: "TourNames",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "TourNames",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TourNames");

            migrationBuilder.DropColumn(
                name: "IsHaveNightRides",
                table: "TourNames");

            migrationBuilder.DropColumn(
                name: "NightRidesCount",
                table: "TourNames");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "TourNames");

            migrationBuilder.AddColumn<bool>(
                name: "IsHaveNightRides",
                table: "Tours",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "NightRidesCount",
                table: "Tours",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
