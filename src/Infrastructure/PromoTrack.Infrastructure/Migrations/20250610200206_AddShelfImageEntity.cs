using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShelfImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShelfImages",
                columns: table => new
                {
                    ShelfImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromoterActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelfImages", x => x.ShelfImageId);
                    table.ForeignKey(
                        name: "FK_ShelfImages_PromoterActivities_PromoterActivityId",
                        column: x => x.PromoterActivityId,
                        principalTable: "PromoterActivities",
                        principalColumn: "PromoterActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShelfImages_PromoterActivityId",
                table: "ShelfImages",
                column: "PromoterActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShelfImages");
        }
    }
}
