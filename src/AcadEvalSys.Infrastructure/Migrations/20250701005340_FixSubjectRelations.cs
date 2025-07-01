using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSubjectRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorCompetencyAssignments_ProfessorId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "CareerYear",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "StudentCompetencyEvaluations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TechnicalCareerId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ProfessorUserId",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentCompetencyEvaluations_SubjectId",
                table: "StudentCompetencyEvaluations",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCompetencyAssignments_ProfessorUserId",
                table: "ProfessorCompetencyAssignments",
                column: "ProfessorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCompetencyAssignments_SubjectId",
                table: "ProfessorCompetencyAssignments",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorUserId",
                table: "ProfessorCompetencyAssignments",
                column: "ProfessorUserId",
                principalTable: "Professors",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Subjects_SubjectId",
                table: "ProfessorCompetencyAssignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments",
                column: "TechnicalCareerId",
                principalTable: "TechnicalCareers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetencyEvaluations_Subjects_SubjectId",
                table: "StudentCompetencyEvaluations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Professors_ProfessorUserId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_Subjects_SubjectId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_TechnicalCareers_TechnicalCa~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetencyEvaluations_Subjects_SubjectId",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_StudentCompetencyEvaluations_SubjectId",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorCompetencyAssignments_ProfessorUserId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorCompetencyAssignments_SubjectId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropColumn(
                name: "ProfessorUserId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.AddColumn<int>(
                name: "CareerYear",
                table: "StudentCompetencyEvaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "TechnicalCareerId",
                table: "ProfessorCompetencyAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessorId",
                table: "ProfessorCompetencyAssignments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ProfessorCompetencyAssignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCompetencyAssignments_ProfessorId",
                table: "ProfessorCompetencyAssignments",
                column: "ProfessorId");

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
        }
    }
}
