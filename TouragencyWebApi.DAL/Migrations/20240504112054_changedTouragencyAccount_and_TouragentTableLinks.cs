using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TouragencyWebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedTouragencyAccount_and_TouragentTableLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TouragencyEmployeeAccounts_TouragencyEmployeeId",
                table: "TouragencyEmployeeAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_TouragencyEmployeeAccounts_TouragencyEmployeeId",
                table: "TouragencyEmployeeAccounts",
                column: "TouragencyEmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TouragencyEmployeeAccounts_TouragencyEmployeeId",
                table: "TouragencyEmployeeAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_TouragencyEmployeeAccounts_TouragencyEmployeeId",
                table: "TouragencyEmployeeAccounts",
                column: "TouragencyEmployeeId");
        }
    }
}
