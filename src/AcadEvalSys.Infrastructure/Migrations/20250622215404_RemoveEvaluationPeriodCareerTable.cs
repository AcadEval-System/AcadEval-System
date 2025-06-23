using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEvaluationPeriodCareerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop table if exists
            migrationBuilder.Sql(@"
                DROP TABLE IF EXISTS ""EvaluationPeriodCareer"" CASCADE;
            ");

            // Drop columns only if they exist - ProfessorCompetencyAssignments
            migrationBuilder.Sql(@"
                ALTER TABLE ""ProfessorCompetencyAssignments"" 
                DROP COLUMN IF EXISTS ""FormName"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""ProfessorCompetencyAssignments"" 
                DROP COLUMN IF EXISTS ""NotificationSentAt"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""ProfessorCompetencyAssignments"" 
                DROP COLUMN IF EXISTS ""Status"";
            ");

            // Drop columns only if they exist - EvaluationPeriods
            migrationBuilder.Sql(@"
                ALTER TABLE ""EvaluationPeriods"" 
                DROP COLUMN IF EXISTS ""NotifyClose"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""EvaluationPeriods"" 
                DROP COLUMN IF EXISTS ""NotifyStart"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""EvaluationPeriods"" 
                DROP COLUMN IF EXISTS ""ReminderFrequency"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""EvaluationPeriods"" 
                DROP COLUMN IF EXISTS ""SendReminders"";
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""EvaluationPeriods"" 
                DROP COLUMN IF EXISTS ""Status"";
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormName",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NotificationSentAt",
                table: "ProfessorCompetencyAssignments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyClose",
                table: "EvaluationPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyStart",
                table: "EvaluationPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReminderFrequency",
                table: "EvaluationPeriods",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SendReminders",
                table: "EvaluationPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "EvaluationPeriods",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationPeriodCareer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    EvaluationPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicalCareerId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPeriodCareer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationPeriodCareer_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationPeriodCareer_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationPeriodCareer_EvaluationPeriods_EvaluationPeriodId",
                        column: x => x.EvaluationPeriodId,
                        principalTable: "EvaluationPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationPeriodCareer_TechnicalCareers_TechnicalCareerId",
                        column: x => x.TechnicalCareerId,
                        principalTable: "TechnicalCareers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriodCareer_CreatedByUserId",
                table: "EvaluationPeriodCareer",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriodCareer_EvaluationPeriodId",
                table: "EvaluationPeriodCareer",
                column: "EvaluationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriodCareer_TechnicalCareerId",
                table: "EvaluationPeriodCareer",
                column: "TechnicalCareerId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriodCareer_UpdatedByUserId",
                table: "EvaluationPeriodCareer",
                column: "UpdatedByUserId");
        }
    }
}
