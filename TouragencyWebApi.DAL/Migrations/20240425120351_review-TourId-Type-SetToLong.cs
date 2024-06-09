using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TouragencyWebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class reviewTourIdTypeSetToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Tours_TourId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TourId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "TourId1",
                table: "Reviews");

            migrationBuilder.AlterColumn<long>(
                name: "TourId",
                table: "Reviews",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TourId",
                table: "Reviews",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Tours_TourId",
                table: "Reviews",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Tours_TourId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TourId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "TourId1",
                table: "Reviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TourId1",
                table: "Reviews",
                column: "TourId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Tours_TourId1",
                table: "Reviews",
                column: "TourId1",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
