using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_StudentCompetencyEvaluations_StudentCompe~",
                table: "QuestionResponses");

            migrationBuilder.DropTable(
                name: "StudentCompetencyEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_StudentCompetencyEvaluationId",
                table: "QuestionResponses");

            migrationBuilder.DropColumn(
                name: "ReportData",
                table: "StudentEvaluationReport");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudentEvaluationReport");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentYear",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "StudentCompetencyCalification",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCompetencyCalification");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentYear",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportData",
                table: "StudentEvaluationReport",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StudentEvaluationReport",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentCompetencyEvaluations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    ProfessorCompetencyAssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    CompetencyLevel = table.Column<int>(type: "integer", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalScore = table.Column<decimal>(type: "numeric", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCompetencyEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyEvaluations_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCompetencyEvaluations_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCompetencyEvaluations_ProfessorCompetencyAssignments~",
                        column: x => x.ProfessorCompetencyAssignmentId,
                        principalTable: "ProfessorCompetencyAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyEvaluations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCompetencyEvaluations_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_StudentCompetencyEvaluationId",
                table: "QuestionResponses",
                column: "StudentCompetencyEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyEvaluations_CreatedByUserId",
                table: "StudentCompetencyEvaluations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyEvaluations_ProfessorCompetencyAssignmentId",
                table: "StudentCompetencyEvaluations",
                column: "ProfessorCompetencyAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyEvaluations_StudentId",
                table: "StudentCompetencyEvaluations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyEvaluations_SubjectId",
                table: "StudentCompetencyEvaluations",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyEvaluations_UpdatedByUserId",
                table: "StudentCompetencyEvaluations",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_StudentCompetencyEvaluations_StudentCompe~",
                table: "QuestionResponses",
                column: "StudentCompetencyEvaluationId",
                principalTable: "StudentCompetencyEvaluations",
                principalColumn: "Id");
        }
    }
}
