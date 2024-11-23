using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaMa.Via.Auctus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "via_auctus");

            migrationBuilder.CreateTable(
                name: "CarBrands",
                schema: "via_auctus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Theme_PrimaryColor = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Theme_SecondaryColor = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Theme_FontFamily = table.Column<string>(type: "text", nullable: false),
                    Theme_LogoUrl = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                schema: "via_auctus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                schema: "via_auctus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CarBrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalSchema: "via_auctus",
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                schema: "via_auctus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FuelTypeId = table.Column<string>(type: "character varying(255)", nullable: false),
                    HorsePower = table.Column<int>(type: "integer", nullable: true),
                    Torque = table.Column<int>(type: "integer", nullable: true),
                    EngineEfficiencyValue = table.Column<decimal>(type: "numeric", nullable: false),
                    EngineEfficiencyUnit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_FuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalSchema: "via_auctus",
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarModelVersions",
                schema: "via_auctus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModelVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModelVersions_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalSchema: "via_auctus",
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "via_auctus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EngineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Registration_LicensePlate = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Registration_FirstRegistrationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Registration_RegistrationExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarBrands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "via_auctus",
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_CarModelVersions_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "via_auctus",
                        principalTable: "CarModelVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_CarModels_ModelId",
                        column: x => x.ModelId,
                        principalSchema: "via_auctus",
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_Engines_EngineId",
                        column: x => x.EngineId,
                        principalSchema: "via_auctus",
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModelVersionEngines",
                schema: "via_auctus",
                columns: table => new
                {
                    EngineId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelVersionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelVersionEngines", x => new { x.EngineId, x.ModelVersionId });
                    table.ForeignKey(
                        name: "FK_ModelVersionEngines_CarModelVersions_ModelVersionId",
                        column: x => x.ModelVersionId,
                        principalSchema: "via_auctus",
                        principalTable: "CarModelVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelVersionEngines_Engines_EngineId",
                        column: x => x.EngineId,
                        principalSchema: "via_auctus",
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "via_auctus",
                table: "FuelTypes",
                column: "Id",
                values: new object[]
                {
                    "Biodiesel",
                    "Compressed Natural Gas (CNG)",
                    "Diesel",
                    "Electric",
                    "Ethanol",
                    "Gasoline",
                    "Hybrid",
                    "Hydrogen Fuel Cell",
                    "Liquefied Natural Gas (LNG)",
                    "Liquefied Petroleum Gas (LPG)",
                    "Plug-in Hybrid",
                    "Propane"
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarBrandId",
                schema: "via_auctus",
                table: "CarModels",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModelVersions_CarModelId",
                schema: "via_auctus",
                table: "CarModelVersions",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                schema: "via_auctus",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineId",
                schema: "via_auctus",
                table: "Cars",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                schema: "via_auctus",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_VersionId",
                schema: "via_auctus",
                table: "Cars",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_FuelTypeId",
                schema: "via_auctus",
                table: "Engines",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelVersionEngines_ModelVersionId",
                schema: "via_auctus",
                table: "ModelVersionEngines",
                column: "ModelVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars",
                schema: "via_auctus");

            migrationBuilder.DropTable(
                name: "ModelVersionEngines",
                schema: "via_auctus");

            migrationBuilder.DropTable(
                name: "CarModelVersions",
                schema: "via_auctus");

            migrationBuilder.DropTable(
                name: "Engines",
                schema: "via_auctus");

            migrationBuilder.DropTable(
                name: "CarModels",
                schema: "via_auctus");

            migrationBuilder.DropTable(
                name: "FuelTypes",
                schema: "via_auctus");

            migrationBuilder.DropTable(
                name: "CarBrands",
                schema: "via_auctus");
        }
    }
}
