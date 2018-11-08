using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class scoresheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoreSheet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    DateEntered = table.Column<DateTime>(nullable: false),
                    EnteredBy = table.Column<string>(nullable: true),
                    ProgrammeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_Programme_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScoreSheet_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_CourseId",
                table: "ScoreSheet",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_ProgrammeId",
                table: "ScoreSheet",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_StudentId",
                table: "ScoreSheet",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreSheet");
        }
    }
}
