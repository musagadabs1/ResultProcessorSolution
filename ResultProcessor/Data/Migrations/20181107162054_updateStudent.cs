using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Data.Migrations
{
    public partial class updateStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateEntered",
                table: "Student",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EnteredBy",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEntered",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EnteredBy",
                table: "Student");
        }
    }
}
