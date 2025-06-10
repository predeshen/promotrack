using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalQuestionEngineEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignQuestionConfigs",
                columns: table => new
                {
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    CampaignQuestionConfigId = table.Column<int>(type: "int", nullable: false),
                    IsActiveForCampaign = table.Column<bool>(type: "bit", nullable: false),
                    IsMandatoryForCampaign = table.Column<bool>(type: "bit", nullable: false),
                    SortOrderForCampaign = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignQuestionConfigs", x => new { x.CampaignId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_CampaignQuestionConfigs_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignQuestionConfigs_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    SurveyAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoterActivityId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.SurveyAnswerId);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_PromoterActivities_PromoterActivityId",
                        column: x => x.PromoterActivityId,
                        principalTable: "PromoterActivities",
                        principalColumn: "PromoterActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswerSelectedOptions",
                columns: table => new
                {
                    SurveyAnswerSelectedOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyAnswerId = table.Column<int>(type: "int", nullable: false),
                    QuestionOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswerSelectedOptions", x => x.SurveyAnswerSelectedOptionId);
                    table.ForeignKey(
                        name: "FK_SurveyAnswerSelectedOptions_QuestionOptions_QuestionOptionId",
                        column: x => x.QuestionOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "QuestionOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyAnswerSelectedOptions_SurveyAnswers_SurveyAnswerId",
                        column: x => x.SurveyAnswerId,
                        principalTable: "SurveyAnswers",
                        principalColumn: "SurveyAnswerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignQuestionConfigs_QuestionId",
                table: "CampaignQuestionConfigs",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_PromoterActivityId",
                table: "SurveyAnswers",
                column: "PromoterActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_QuestionId",
                table: "SurveyAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswerSelectedOptions_QuestionOptionId",
                table: "SurveyAnswerSelectedOptions",
                column: "QuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswerSelectedOptions_SurveyAnswerId",
                table: "SurveyAnswerSelectedOptions",
                column: "SurveyAnswerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignQuestionConfigs");

            migrationBuilder.DropTable(
                name: "SurveyAnswerSelectedOptions");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");
        }
    }
}
