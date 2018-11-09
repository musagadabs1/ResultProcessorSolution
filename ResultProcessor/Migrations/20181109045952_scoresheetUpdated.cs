using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResultProcessor.Migrations
{
    public partial class scoresheetUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreSheet_Programme_ProgrammeId",
                table: "ScoreSheet");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammeId",
                table: "ScoreSheet",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ScoreSheet",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ScoreSheet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreSheet_Programme_ProgrammeId",
                table: "ScoreSheet",
                column: "ProgrammeId",
                principalTable: "Programme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreSheet_Programme_ProgrammeId",
                table: "ScoreSheet");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ScoreSheet");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ScoreSheet");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammeId",
                table: "ScoreSheet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreSheet_Programme_ProgrammeId",
                table: "ScoreSheet",
                column: "ProgrammeId",
                principalTable: "Programme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
