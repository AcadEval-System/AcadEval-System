using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReport_AspNetUsers_CreatedByUserId",
                table: "StudentEvaluationReport");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReport_AspNetUsers_UpdatedByUserId",
                table: "StudentEvaluationReport");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReport_CompetenciesEvaluationInstances_Com~",
                table: "StudentEvaluationReport");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReport_Students_StudentId",
                table: "StudentEvaluationReport");

            migrationBuilder.DropTable(
                name: "CompetenciesEvaluationInstanceTechnicalCareer");

            migrationBuilder.DropTable(
                name: "CompetenciesEvaluationInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEvaluationReport",
                table: "StudentEvaluationReport");

            migrationBuilder.DropIndex(
                name: "IX_StudentEvaluationReport_CompetenciesEvaluationInstanceId",
                table: "StudentEvaluationReport");

            migrationBuilder.RenameTable(
                name: "StudentEvaluationReport",
                newName: "StudentEvaluationReports");

            migrationBuilder.RenameColumn(
                name: "CompetenciesEvaluationInstanceId",
                table: "ProfessorCompetencyAssignments",
                newName: "CompetencyEvaluationInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~",
                table: "ProfessorCompetencyAssignments",
                newName: "IX_ProfessorCompetencyAssignments_CompetencyEvaluationInstance~");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReport_UpdatedByUserId",
                table: "StudentEvaluationReports",
                newName: "IX_StudentEvaluationReports_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReport_StudentId",
                table: "StudentEvaluationReports",
                newName: "IX_StudentEvaluationReports_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReport_CreatedByUserId",
                table: "StudentEvaluationReports",
                newName: "IX_StudentEvaluationReports_CreatedByUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompetencyEvaluationInstanceId",
                table: "StudentEvaluationReports",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEvaluationReports",
                table: "StudentEvaluationReports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CompetencyEvaluationInstances",
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
                    table.PrimaryKey("PK_CompetencyEvaluationInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyEvaluationInstances_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetencyEvaluationInstances_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompetencyEvaluationInstanceTechnicalCareer",
                columns: table => new
                {
                    CompetencyEvaluationInstancesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicalCareersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyEvaluationInstanceTechnicalCareer", x => new { x.CompetencyEvaluationInstancesId, x.TechnicalCareersId });
                    table.ForeignKey(
                        name: "FK_CompetencyEvaluationInstanceTechnicalCareer_CompetencyEvalu~",
                        column: x => x.CompetencyEvaluationInstancesId,
                        principalTable: "CompetencyEvaluationInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyEvaluationInstanceTechnicalCareer_TechnicalCareer~",
                        column: x => x.TechnicalCareersId,
                        principalTable: "TechnicalCareers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvaluationReports_CompetencyEvaluationInstanceId",
                table: "StudentEvaluationReports",
                column: "CompetencyEvaluationInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyEvaluationInstances_CreatedByUserId",
                table: "CompetencyEvaluationInstances",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyEvaluationInstances_UpdatedByUserId",
                table: "CompetencyEvaluationInstances",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyEvaluationInstanceTechnicalCareer_TechnicalCareer~",
                table: "CompetencyEvaluationInstanceTechnicalCareer",
                column: "TechnicalCareersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_CompetencyEvaluationInstance~",
                table: "ProfessorCompetencyAssignments",
                column: "CompetencyEvaluationInstanceId",
                principalTable: "CompetencyEvaluationInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReports_AspNetUsers_CreatedByUserId",
                table: "StudentEvaluationReports",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReports_AspNetUsers_UpdatedByUserId",
                table: "StudentEvaluationReports",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReports_CompetencyEvaluationInstances_Comp~",
                table: "StudentEvaluationReports",
                column: "CompetencyEvaluationInstanceId",
                principalTable: "CompetencyEvaluationInstances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReports_Students_StudentId",
                table: "StudentEvaluationReports",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorCompetencyAssignments_CompetencyEvaluationInstance~",
                table: "ProfessorCompetencyAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReports_AspNetUsers_CreatedByUserId",
                table: "StudentEvaluationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReports_AspNetUsers_UpdatedByUserId",
                table: "StudentEvaluationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReports_CompetencyEvaluationInstances_Comp~",
                table: "StudentEvaluationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEvaluationReports_Students_StudentId",
                table: "StudentEvaluationReports");

            migrationBuilder.DropTable(
                name: "CompetencyEvaluationInstanceTechnicalCareer");

            migrationBuilder.DropTable(
                name: "CompetencyEvaluationInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEvaluationReports",
                table: "StudentEvaluationReports");

            migrationBuilder.DropIndex(
                name: "IX_StudentEvaluationReports_CompetencyEvaluationInstanceId",
                table: "StudentEvaluationReports");

            migrationBuilder.DropColumn(
                name: "CompetencyEvaluationInstanceId",
                table: "StudentEvaluationReports");

            migrationBuilder.RenameTable(
                name: "StudentEvaluationReports",
                newName: "StudentEvaluationReport");

            migrationBuilder.RenameColumn(
                name: "CompetencyEvaluationInstanceId",
                table: "ProfessorCompetencyAssignments",
                newName: "CompetenciesEvaluationInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorCompetencyAssignments_CompetencyEvaluationInstance~",
                table: "ProfessorCompetencyAssignments",
                newName: "IX_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReports_UpdatedByUserId",
                table: "StudentEvaluationReport",
                newName: "IX_StudentEvaluationReport_UpdatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReports_StudentId",
                table: "StudentEvaluationReport",
                newName: "IX_StudentEvaluationReport_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEvaluationReports_CreatedByUserId",
                table: "StudentEvaluationReport",
                newName: "IX_StudentEvaluationReport_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEvaluationReport",
                table: "StudentEvaluationReport",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CompetenciesEvaluationInstances",
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
                name: "IX_StudentEvaluationReport_CompetenciesEvaluationInstanceId",
                table: "StudentEvaluationReport",
                column: "CompetenciesEvaluationInstanceId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorCompetencyAssignments_CompetenciesEvaluationInstan~",
                table: "ProfessorCompetencyAssignments",
                column: "CompetenciesEvaluationInstanceId",
                principalTable: "CompetenciesEvaluationInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReport_AspNetUsers_CreatedByUserId",
                table: "StudentEvaluationReport",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReport_AspNetUsers_UpdatedByUserId",
                table: "StudentEvaluationReport",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReport_CompetenciesEvaluationInstances_Com~",
                table: "StudentEvaluationReport",
                column: "CompetenciesEvaluationInstanceId",
                principalTable: "CompetenciesEvaluationInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEvaluationReport_Students_StudentId",
                table: "StudentEvaluationReport",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
