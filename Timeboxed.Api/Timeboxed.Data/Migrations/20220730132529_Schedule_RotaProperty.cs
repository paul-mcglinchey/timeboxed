﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Schedule_RotaProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rotas_RotaId",
                table: "Schedules");

            migrationBuilder.AlterColumn<Guid>(
                name: "RotaId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rotas_RotaId",
                table: "Schedules",
                column: "RotaId",
                principalTable: "Rotas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rotas_RotaId",
                table: "Schedules");

            migrationBuilder.AlterColumn<Guid>(
                name: "RotaId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rotas_RotaId",
                table: "Schedules",
                column: "RotaId",
                principalTable: "Rotas",
                principalColumn: "Id");
        }
    }
}
