using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_EvaluationPeriods_Evaluation~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReport_EvaluationPeriods_EvaluationPeriodId",
                table: "StudentEvaluationReport");

            migrationBuilder.DropTable(
                name: "EvaluationPeriodTechnicalCareer");

            migrationBuilder.DropTable(
                name: "StudentCompetencyCalification");

            migrationBuilder.DropTable(
                name: "EvaluationPeriods");

            migrationBuilder.RenameColumn(
                name: "EvaluationPeriodId",
                table: "StudentEvaluationReport",
                newName: "CompetenciesEvaluationInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReport_EvaluationPeriodId",
                table: "StudentEvaluationReport",
                newName: "IX_StudentEvaluationReport_CompetenciesEvaluationInstanceId");

            migrationBuilder.RenameColumn(
                name: "EvaluationPeriodId",
                table: "ProfessorCompetencyAssignments",
                newName: "CompetenciesEvaluationInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorCompetencyAssignments_EvaluationPeriodId",
                table: "ProfessorCompetencyAssignments",
                newName: "IX_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~");

            migrationBuilder.CreateTable(
                name: "CompetenciesEvaluationInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PeriodFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenciesEvaluationInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenciesEvaluationInstances_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetenciesEvaluationInstances_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentCompetencyAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: true),
                    ProfessorCompetencyAssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CompetencyLevel = table.Column<int>(type: "integer", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCompetencyAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyAssessments_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCompetencyAssessments_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCompetencyAssessments_ProfessorCompetencyAssignments~",
                        column: x => x.ProfessorCompetencyAssignmentId,
                        principalTable: "ProfessorCompetencyAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyAssessments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyAssessments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompetenciesEvaluationInstanceTechnicalCareer",
                columns: table => new
                {
                    CompetenciesEvaluationInstancesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicalCareersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenciesEvaluationInstanceTechnicalCareer", x => new { x.CompetenciesEvaluationInstancesId, x.TechnicalCareersId });
                    table.ForeignKey(
                        name: "FK_CompetenciesEvaluationInstanceTechnicalCareer_CompetenciesE~",
                        column: x => x.CompetenciesEvaluationInstancesId,
                        principalTable: "CompetenciesEvaluationInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenciesEvaluationInstanceTechnicalCareer_TechnicalCare~",
                        column: x => x.TechnicalCareersId,
                        principalTable: "TechnicalCareers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetenciesEvaluationInstances_CreatedByUserId",
                table: "CompetenciesEvaluationInstances",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenciesEvaluationInstances_UpdatedByUserId",
                table: "CompetenciesEvaluationInstances",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenciesEvaluationInstanceTechnicalCareer_TechnicalCare~",
                table: "CompetenciesEvaluationInstanceTechnicalCareer",
                column: "TechnicalCareersId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyAssessments_CreatedByUserId",
                table: "StudentCompetencyAssessments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyAssessments_ProfessorCompetencyAssignmentId",
                table: "StudentCompetencyAssessments",
                column: "ProfessorCompetencyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyAssessments_StudentId",
                table: "StudentCompetencyAssessments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyAssessments_SubjectId",
                table: "StudentCompetencyAssessments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyAssessments_UpdatedByUserId",
                table: "StudentCompetencyAssessments",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~",
                table: "ProfessorCompetencyAssignments",
                column: "CompetenciesEvaluationInstanceId",
                principalTable: "CompetenciesEvaluationInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReport_CompetenciesEvaluationInstances_Com~",
                table: "StudentEvaluationReport",
                column: "CompetenciesEvaluationInstanceId",
                principalTable: "CompetenciesEvaluationInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReport_CompetenciesEvaluationInstances_Com~",
                table: "StudentEvaluationReport");

            migrationBuilder.DropTable(
                name: "CompetenciesEvaluationInstanceTechnicalCareer");

            migrationBuilder.DropTable(
                name: "StudentCompetencyAssessments");

            migrationBuilder.DropTable(
                name: "CompetenciesEvaluationInstances");

            migrationBuilder.RenameColumn(
                name: "CompetenciesEvaluationInstanceId",
                table: "StudentEvaluationReport",
                newName: "EvaluationPeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReport_CompetenciesEvaluationInstanceId",
                table: "StudentEvaluationReport",
                newName: "IX_StudentEvaluationReport_EvaluationPeriodId");

            migrationBuilder.RenameColumn(
                name: "CompetenciesEvaluationInstanceId",
                table: "ProfessorCompetencyAssignments",
                newName: "EvaluationPeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~",
                table: "ProfessorCompetencyAssignments",
                newName: "IX_ProfessorCompetencyAssignments_EvaluationPeriodId");

            migrationBuilder.CreateTable(
                name: "EvaluationPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PeriodFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationPeriods_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationPeriods_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentCompetencyCalification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    ProfessorCompetencyAssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CompetencyLevel = table.Column<int>(type: "integer", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCompetencyCalification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyCalification_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCompetencyCalification_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCompetencyCalification_ProfessorCompetencyAssignment~",
                        column: x => x.ProfessorCompetencyAssignmentId,
                        principalTable: "ProfessorCompetencyAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyCalification_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyCalification_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriods_CreatedByUserId",
                table: "EvaluationPeriods",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriods_UpdatedByUserId",
                table: "EvaluationPeriods",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriodTechnicalCareer_TechnicalCareersId",
                table: "EvaluationPeriodTechnicalCareer",
                column: "TechnicalCareersId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyCalification_CreatedByUserId",
                table: "StudentCompetencyCalification",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyCalification_ProfessorCompetencyAssignment~",
                table: "StudentCompetencyCalification",
                column: "ProfessorCompetencyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyCalification_StudentId",
                table: "StudentCompetencyCalification",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyCalification_SubjectId",
                table: "StudentCompetencyCalification",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyCalification_UpdatedByUserId",
                table: "StudentCompetencyCalification",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_EvaluationPeriods_Evaluation~",
                table: "ProfessorCompetencyAssignments",
                column: "EvaluationPeriodId",
                principalTable: "EvaluationPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReport_EvaluationPeriods_EvaluationPeriodId",
                table: "StudentEvaluationReport",
                column: "EvaluationPeriodId",
                principalTable: "EvaluationPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
