using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandQuestionDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandQuestionDefaults",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    BrandQuestionDefaultId = table.Column<int>(type: "int", nullable: false),
                    IsMandatoryByDefault = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandQuestionDefaults", x => new { x.BrandId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_BrandQuestionDefaults_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandQuestionDefaults_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandQuestionDefaults_QuestionId",
                table: "BrandQuestionDefaults",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandQuestionDefaults");
        }
    }
}
