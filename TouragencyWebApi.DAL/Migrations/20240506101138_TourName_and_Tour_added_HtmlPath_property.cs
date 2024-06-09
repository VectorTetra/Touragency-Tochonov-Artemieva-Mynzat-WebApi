using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TouragencyWebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TourName_and_Tour_added_HtmlPath_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HtmlPagePathUrl",
                table: "TourNames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlPagePathUrl",
                table: "TourNames");
        }
    }
}
