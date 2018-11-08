using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Data.Migrations
{
    public partial class updateAllModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Department_DepartmentId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "ScoreSheet");

            migrationBuilder.DropIndex(
                name: "IX_Student_DepartmentId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Student");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RegNo",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RegNo",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ScoreSheet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    DateEntered = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    DeptId = table.Column<int>(nullable: false),
                    EnteredBy = table.Column<string>(nullable: true),
                    Grade = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ProgrammeId = table.Column<int>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
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
                        name: "FK_ScoreSheet_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Student_DepartmentId",
                table: "Student",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_CourseId",
                table: "ScoreSheet",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_DepartmentId",
                table: "ScoreSheet",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_ProgrammeId",
                table: "ScoreSheet",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSheet_StudentId",
                table: "ScoreSheet",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Department_DepartmentId",
                table: "Student",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
