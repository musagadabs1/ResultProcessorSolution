using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class programmeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programme_Department_DepartmentId",
                table: "Programme");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Programme");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Programme",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Programme_Department_DepartmentId",
                table: "Programme",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programme_Department_DepartmentId",
                table: "Programme");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Programme",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Programme",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Programme_Department_DepartmentId",
                table: "Programme",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
