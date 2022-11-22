using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class ContactInfo_PrimaryInfo_Strings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfos_Emails_PrimaryEmailAddressId",
                table: "ContactInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfos_PhoneNumbers_PrimaryPhoneNumberId",
                table: "ContactInfos");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfos_PrimaryEmailAddressId",
                table: "ContactInfos");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfos_PrimaryPhoneNumberId",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "PrimaryEmailAddressId",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneNumberId",
                table: "ContactInfos");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryEmailAddress",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhoneNumber",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "PrimaryEmailAddress",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneNumber",
                table: "ContactInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryEmailAddressId",
                table: "ContactInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryPhoneNumberId",
                table: "ContactInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PrimaryEmailAddressId",
                table: "ContactInfos",
                column: "PrimaryEmailAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PrimaryPhoneNumberId",
                table: "ContactInfos",
                column: "PrimaryPhoneNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfos_Emails_PrimaryEmailAddressId",
                table: "ContactInfos",
                column: "PrimaryEmailAddressId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfos_PhoneNumbers_PrimaryPhoneNumberId",
                table: "ContactInfos",
                column: "PrimaryPhoneNumberId",
                principalTable: "PhoneNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
