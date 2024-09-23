using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class QuickUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HangfireJobId",
                table: "dbo_job",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "HangfireJobId",
                table: "audit_job",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "dbo_matrix",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTime(2024, 9, 21, 20, 54, 40, 303, DateTimeKind.Local).AddTicks(6896), new DateTime(2024, 9, 21, 20, 54, 40, 303, DateTimeKind.Local).AddTicks(6942) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "dbo_job",
                keyColumn: "HangfireJobId",
                keyValue: null,
                column: "HangfireJobId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "HangfireJobId",
                table: "dbo_job",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "audit_job",
                keyColumn: "HangfireJobId",
                keyValue: null,
                column: "HangfireJobId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "HangfireJobId",
                table: "audit_job",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "dbo_matrix",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "ModifyDate" },
                values: new object[] { new DateTime(2024, 9, 21, 19, 19, 53, 244, DateTimeKind.Local).AddTicks(8486), new DateTime(2024, 9, 21, 19, 19, 53, 244, DateTimeKind.Local).AddTicks(8536) });
        }
    }
}
