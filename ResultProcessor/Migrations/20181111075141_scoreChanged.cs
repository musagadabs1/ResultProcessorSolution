using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class scoreChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "ScoreSheet",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Score",
                table: "ScoreSheet",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
