using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class RemovedEmployeePreferencesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeUnavailableDays_EmployeePreferences_EmployeePreferencesId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeUnavailableDays_Employees_EmployeeId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.DropTable(
                name: "EmployeePreferences");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeUnavailableDays_EmployeePreferencesId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.DropColumn(
                name: "EmployeePreferencesId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "EmployeeUnavailableDays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "DayOfWeek",
                table: "EmployeeUnavailableDays",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeUnavailableDays_Employees_EmployeeId",
                table: "EmployeeUnavailableDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeUnavailableDays_Employees_EmployeeId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "EmployeeUnavailableDays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "DayOfWeek",
                table: "EmployeeUnavailableDays",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeePreferencesId",
                table: "EmployeeUnavailableDays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EmployeePreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxHours = table.Column<int>(type: "int", nullable: true),
                    MinHours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePreferences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUnavailableDays_EmployeePreferencesId",
                table: "EmployeeUnavailableDays",
                column: "EmployeePreferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeUnavailableDays_EmployeePreferences_EmployeePreferencesId",
                table: "EmployeeUnavailableDays",
                column: "EmployeePreferencesId",
                principalTable: "EmployeePreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeUnavailableDays_Employees_EmployeeId",
                table: "EmployeeUnavailableDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
