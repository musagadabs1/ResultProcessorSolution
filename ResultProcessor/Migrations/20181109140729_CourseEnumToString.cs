using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class CourseEnumToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Semester",
                table: "Course",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Course",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Semester",
                table: "Course",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Course",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
