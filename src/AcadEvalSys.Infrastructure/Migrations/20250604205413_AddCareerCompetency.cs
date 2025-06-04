using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcadEvalSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCareerCompetency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_TechnicalCareers_TechnicalCareerId",
                table: "Competencies");

            migrationBuilder.DropIndex(
                name: "IX_Competencies_TechnicalCareerId",
                table: "Competencies");

            migrationBuilder.DropColumn(
                name: "TechnicalCareerId",
                table: "Competencies");

            migrationBuilder.Sql("ALTER TABLE \"Competencies\" ALTER COLUMN \"Type\" TYPE integer USING \"Type\"::integer;");
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Competencies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerCompetencies");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Competencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "TechnicalCareerId",
                table: "Competencies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_TechnicalCareerId",
                table: "Competencies",
                column: "TechnicalCareerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_TechnicalCareers_TechnicalCareerId",
                table: "Competencies",
                column: "TechnicalCareerId",
                principalTable: "TechnicalCareers",
                principalColumn: "Id");
        }
    }
}
