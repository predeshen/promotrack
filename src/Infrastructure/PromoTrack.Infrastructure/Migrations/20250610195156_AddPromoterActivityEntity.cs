using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPromoterActivityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromoterActivities",
                columns: table => new
                {
                    PromoterActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CheckInTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckInLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckInLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckOutTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckOutLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoterActivities", x => x.PromoterActivityId);
                    table.ForeignKey(
                        name: "FK_PromoterActivities_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoterActivities_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoterActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromoterActivities_CampaignId",
                table: "PromoterActivities",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoterActivities_StoreId",
                table: "PromoterActivities",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoterActivities_UserId",
                table: "PromoterActivities",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromoterActivities");
        }
    }
}
