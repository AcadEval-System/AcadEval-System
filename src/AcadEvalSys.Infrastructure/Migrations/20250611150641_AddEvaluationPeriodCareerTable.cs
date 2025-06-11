using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEvaluationPeriodCareerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Competencies_CompetencyId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_EvaluationPeriods_Evaluation~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "CompetencyName",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "TechnicalCareerName",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.AddColumn<int>(
                name: "CurrentYear",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TechnicalCareerId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetencyLevel",
                table: "StudentCompetencyEvaluations",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TechnicalCareerId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorId",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EvaluationPeriodId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompetencyId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CompetencyLevelDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyLevelDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyLevelDescriptions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetencyLevelDescriptions_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetencyLevelDescriptions_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationPeriodCareer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluationPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicalCareerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "EvaluationPeriodTechnicalCareer",
                columns: table => new
                {
                    EvaluationPeriodsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicalCareersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPeriodTechnicalCareer", x => new { x.EvaluationPeriodsId, x.TechnicalCareersId });
                    table.ForeignKey(
                        name: "FK_EvaluationPeriodTechnicalCareer_EvaluationPeriods_Evaluatio~",
                        column: x => x.EvaluationPeriodsId,
                        principalTable: "EvaluationPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationPeriodTechnicalCareer_TechnicalCareers_TechnicalC~",
                        column: x => x.TechnicalCareersId,
                        principalTable: "TechnicalCareers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEvaluationReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    EvaluationPeriodId = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReportData = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvaluationReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationReport_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentEvaluationReport_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentEvaluationReport_EvaluationPeriods_EvaluationPeriodId",
                        column: x => x.EvaluationPeriodId,
                        principalTable: "EvaluationPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEvaluationReport_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_TechnicalCareerId",
                table: "Students",
                column: "TechnicalCareerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyLevelDescriptions_CompetencyId_Level",
                table: "CompetencyLevelDescriptions",
                columns: new[] { "CompetencyId", "Level" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyLevelDescriptions_CreatedByUserId",
                table: "CompetencyLevelDescriptions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyLevelDescriptions_UpdatedByUserId",
                table: "CompetencyLevelDescriptions",
                column: "UpdatedByUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriodTechnicalCareer_TechnicalCareersId",
                table: "EvaluationPeriodTechnicalCareer",
                column: "TechnicalCareersId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationReport_CreatedByUserId",
                table: "StudentEvaluationReport",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationReport_EvaluationPeriodId",
                table: "StudentEvaluationReport",
                column: "EvaluationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationReport_StudentId",
                table: "StudentEvaluationReport",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationReport_UpdatedByUserId",
                table: "StudentEvaluationReport",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Competencies_CompetencyId",
                table: "ProfessorCompetencyAssignments",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_EvaluationPeriods_Evaluation~",
                table: "ProfessorCompetencyAssignments",
                column: "EvaluationPeriodId",
                principalTable: "EvaluationPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorId",
                table: "ProfessorCompetencyAssignments",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments",
                column: "TechnicalCareerId",
                principalTable: "TechnicalCareers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_TechnicalCareers_TechnicalCareerId",
                table: "Students",
                column: "TechnicalCareerId",
                principalTable: "TechnicalCareers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Competencies_CompetencyId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_EvaluationPeriods_Evaluation~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_TechnicalCareers_TechnicalCareerId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CompetencyLevelDescriptions");

            migrationBuilder.DropTable(
                name: "EvaluationPeriodCareer");

            migrationBuilder.DropTable(
                name: "EvaluationPeriodTechnicalCareer");

            migrationBuilder.DropTable(
                name: "StudentEvaluationReport");

            migrationBuilder.DropIndex(
                name: "IX_Students_TechnicalCareerId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurrentYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TechnicalCareerId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CompetencyLevel",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TechnicalCareerId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorId",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvaluationPeriodId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompetencyId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "CompetencyName",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EvaluationId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalCareerName",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Competencies_CompetencyId",
                table: "ProfessorCompetencyAssignments",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_EvaluationPeriods_Evaluation~",
                table: "ProfessorCompetencyAssignments",
                column: "EvaluationPeriodId",
                principalTable: "EvaluationPeriods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorId",
                table: "ProfessorCompetencyAssignments",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments",
                column: "TechnicalCareerId",
                principalTable: "TechnicalCareers",
                principalColumn: "Id");
        }
    }
}
