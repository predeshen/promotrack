using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExtractedProductDataEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtractedProductData",
                columns: table => new
                {
                    ExtractedProductDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoterActivityId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityOnShelf = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ItemsSold = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractedProductData", x => x.ExtractedProductDataId);
                    table.ForeignKey(
                        name: "FK_ExtractedProductData_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtractedProductData_PromoterActivities_PromoterActivityId",
                        column: x => x.PromoterActivityId,
                        principalTable: "PromoterActivities",
                        principalColumn: "PromoterActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtractedProductData_ProductId",
                table: "ExtractedProductData",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractedProductData_PromoterActivityId",
                table: "ExtractedProductData",
                column: "PromoterActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtractedProductData");
        }
    }
}
