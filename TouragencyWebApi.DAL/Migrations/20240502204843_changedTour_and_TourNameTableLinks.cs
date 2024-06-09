using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TouragencyWebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedTour_and_TourNameTableLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelTour");

            migrationBuilder.DropTable(
                name: "SettlementTour");

            migrationBuilder.DropTable(
                name: "TourTransportType");

            migrationBuilder.CreateTable(
                name: "CountryTourName",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "int", nullable: false),
                    TourNamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTourName", x => new { x.CountriesId, x.TourNamesId });
                    table.ForeignKey(
                        name: "FK_CountryTourName_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTourName_TourNames_TourNamesId",
                        column: x => x.TourNamesId,
                        principalTable: "TourNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelTourName",
                columns: table => new
                {
                    HotelsId = table.Column<int>(type: "int", nullable: false),
                    TourNamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelTourName", x => new { x.HotelsId, x.TourNamesId });
                    table.ForeignKey(
                        name: "FK_HotelTourName_Hotels_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelTourName_TourNames_TourNamesId",
                        column: x => x.TourNamesId,
                        principalTable: "TourNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementTourName",
                columns: table => new
                {
                    SettlementsId = table.Column<int>(type: "int", nullable: false),
                    TourNamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementTourName", x => new { x.SettlementsId, x.TourNamesId });
                    table.ForeignKey(
                        name: "FK_SettlementTourName_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementTourName_TourNames_TourNamesId",
                        column: x => x.TourNamesId,
                        principalTable: "TourNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourNameTransportType",
                columns: table => new
                {
                    TourNamesId = table.Column<int>(type: "int", nullable: false),
                    TransportTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourNameTransportType", x => new { x.TourNamesId, x.TransportTypesId });
                    table.ForeignKey(
                        name: "FK_TourNameTransportType_TourNames_TourNamesId",
                        column: x => x.TourNamesId,
                        principalTable: "TourNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourNameTransportType_TransportTypes_TransportTypesId",
                        column: x => x.TransportTypesId,
                        principalTable: "TransportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryTourName_TourNamesId",
                table: "CountryTourName",
                column: "TourNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelTourName_TourNamesId",
                table: "HotelTourName",
                column: "TourNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementTourName_TourNamesId",
                table: "SettlementTourName",
                column: "TourNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_TourNameTransportType_TransportTypesId",
                table: "TourNameTransportType",
                column: "TransportTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryTourName");

            migrationBuilder.DropTable(
                name: "HotelTourName");

            migrationBuilder.DropTable(
                name: "SettlementTourName");

            migrationBuilder.DropTable(
                name: "TourNameTransportType");

            migrationBuilder.CreateTable(
                name: "HotelTour",
                columns: table => new
                {
                    HotelsId = table.Column<int>(type: "int", nullable: false),
                    ToursId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelTour", x => new { x.HotelsId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_HotelTour_Hotels_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelTour_Tours_ToursId",
                        column: x => x.ToursId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementTour",
                columns: table => new
                {
                    SettlementsId = table.Column<int>(type: "int", nullable: false),
                    ToursId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementTour", x => new { x.SettlementsId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_SettlementTour_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementTour_Tours_ToursId",
                        column: x => x.ToursId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTransportType",
                columns: table => new
                {
                    ToursId = table.Column<long>(type: "bigint", nullable: false),
                    TransportTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTransportType", x => new { x.ToursId, x.TransportTypesId });
                    table.ForeignKey(
                        name: "FK_TourTransportType_Tours_ToursId",
                        column: x => x.ToursId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTransportType_TransportTypes_TransportTypesId",
                        column: x => x.TransportTypesId,
                        principalTable: "TransportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelTour_ToursId",
                table: "HotelTour",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementTour_ToursId",
                table: "SettlementTour",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTransportType_TransportTypesId",
                table: "TourTransportType",
                column: "TransportTypesId");
        }
    }
}
