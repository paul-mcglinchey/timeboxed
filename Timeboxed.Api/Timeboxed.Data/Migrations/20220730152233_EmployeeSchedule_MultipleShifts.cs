using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class EmployeeSchedule_MultipleShifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSchedules_EmployeeScheduleShifts_ShiftId",
                table: "EmployeeSchedules");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSchedules_ShiftId",
                table: "EmployeeSchedules");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "EmployeeSchedules");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Schedules",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "EmployeeScheduleShifts",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeScheduleId",
                table: "EmployeeScheduleShifts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeScheduleShifts_EmployeeScheduleId",
                table: "EmployeeScheduleShifts",
                column: "EmployeeScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeScheduleShifts_EmployeeSchedules_EmployeeScheduleId",
                table: "EmployeeScheduleShifts",
                column: "EmployeeScheduleId",
                principalTable: "EmployeeSchedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeScheduleShifts_EmployeeSchedules_EmployeeScheduleId",
                table: "EmployeeScheduleShifts");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeScheduleShifts_EmployeeScheduleId",
                table: "EmployeeScheduleShifts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "EmployeeScheduleShifts");

            migrationBuilder.DropColumn(
                name: "EmployeeScheduleId",
                table: "EmployeeScheduleShifts");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Schedules",
                newName: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftId",
                table: "EmployeeSchedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSchedules_ShiftId",
                table: "EmployeeSchedules",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSchedules_EmployeeScheduleShifts_ShiftId",
                table: "EmployeeSchedules",
                column: "ShiftId",
                principalTable: "EmployeeScheduleShifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
