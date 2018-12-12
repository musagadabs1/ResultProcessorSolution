using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class addGradePoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCreditUnit",
                table: "ProcessedResult",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGradePoint",
                table: "ProcessedResult",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCreditUnit",
                table: "ProcessedResult");

            migrationBuilder.DropColumn(
                name: "TotalGradePoint",
                table: "ProcessedResult");
        }
    }
}
