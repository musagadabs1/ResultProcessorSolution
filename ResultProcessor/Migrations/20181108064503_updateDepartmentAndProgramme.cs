using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class updateDepartmentAndProgramme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Programme",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programme_DepartmentId",
                table: "Programme",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programme_Department_DepartmentId",
                table: "Programme",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programme_Department_DepartmentId",
                table: "Programme");

            migrationBuilder.DropIndex(
                name: "IX_Programme_DepartmentId",
                table: "Programme");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Programme");
        }
    }
}
