using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class KnightRelationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "dbo_puzzle_knightMoves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "audit_puzzle_knightMoves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "dbo_matrix",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTime(2024, 9, 22, 11, 45, 54, 44, DateTimeKind.Local).AddTicks(9458), new DateTime(2024, 9, 22, 11, 45, 54, 44, DateTimeKind.Local).AddTicks(9501) });

            migrationBuilder.CreateIndex(
                name: "IX_dbo_puzzle_knightMoves_JobId",
                table: "dbo_puzzle_knightMoves",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo_puzzle_knightMoves_dbo_job_JobId",
                table: "dbo_puzzle_knightMoves",
                column: "JobId",
                principalTable: "dbo_job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo_puzzle_knightMoves_dbo_job_JobId",
                table: "dbo_puzzle_knightMoves");

            migrationBuilder.DropIndex(
                name: "IX_dbo_puzzle_knightMoves_JobId",
                table: "dbo_puzzle_knightMoves");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "dbo_puzzle_knightMoves");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "audit_puzzle_knightMoves");

            migrationBuilder.UpdateData(
                table: "dbo_matrix",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTime(2024, 9, 21, 20, 54, 40, 303, DateTimeKind.Local).AddTicks(6896), new DateTime(2024, 9, 21, 20, 54, 40, 303, DateTimeKind.Local).AddTicks(6942) });
        }
    }
}
