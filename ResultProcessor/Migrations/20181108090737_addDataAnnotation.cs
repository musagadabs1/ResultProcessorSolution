using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class addDataAnnotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeptName",
                table: "Department",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeptName",
                table: "Department",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
