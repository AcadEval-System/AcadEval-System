using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCareerCompetency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetencyEvaluations_ProfessorCompetencyAssignments~",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetencyEvaluations_Students_StudentId",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropTable(
                name: "CareerCompetencies");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessorCompetencyAssignmentId",
                table: "StudentCompetencyEvaluations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CareerYear",
                table: "StudentCompetencyEvaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Competencies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Competencies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CompetencyTechnicalCareer",
                columns: table => new
                {
                    CompetenciesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicalCareersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyTechnicalCareer", x => new { x.CompetenciesId, x.TechnicalCareersId });
                    table.ForeignKey(
                        name: "FK_CompetencyTechnicalCareer_Competencies_CompetenciesId",
                        column: x => x.CompetenciesId,
                        principalTable: "Competencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyTechnicalCareer_TechnicalCareers_TechnicalCareers~",
                        column: x => x.TechnicalCareersId,
                        principalTable: "TechnicalCareers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyTechnicalCareer_TechnicalCareersId",
                table: "CompetencyTechnicalCareer",
                column: "TechnicalCareersId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetencyEvaluations_ProfessorCompetencyAssignments~",
                table: "StudentCompetencyEvaluations",
                column: "ProfessorCompetencyAssignmentId",
                principalTable: "ProfessorCompetencyAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetencyEvaluations_Students_StudentId",
                table: "StudentCompetencyEvaluations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetencyEvaluations_ProfessorCompetencyAssignments~",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCompetencyEvaluations_Students_StudentId",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.DropTable(
                name: "CompetencyTechnicalCareer");

            migrationBuilder.DropColumn(
                name: "CareerYear",
                table: "StudentCompetencyEvaluations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessorCompetencyAssignmentId",
                table: "StudentCompetencyEvaluations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "StudentCompetencyEvaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Competencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Competencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "CareerCompetencies",
                columns: table => new
                {
                    CareerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CareerYear = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerCompetencies", x => new { x.CareerId, x.CompetencyId, x.CareerYear });
                    table.ForeignKey(
                        name: "FK_CareerCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerCompetencies_TechnicalCareers_CareerId",
                        column: x => x.CareerId,
                        principalTable: "TechnicalCareers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareerCompetencies_CompetencyId",
                table: "CareerCompetencies",
                column: "CompetencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetencyEvaluations_ProfessorCompetencyAssignments~",
                table: "StudentCompetencyEvaluations",
                column: "ProfessorCompetencyAssignmentId",
                principalTable: "ProfessorCompetencyAssignments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCompetencyEvaluations_Students_StudentId",
                table: "StudentCompetencyEvaluations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId");
        }
    }
}
