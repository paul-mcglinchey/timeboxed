using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_GroupUserPermissions_M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications");

            migrationBuilder.DropTable(
                name: "GroupUserPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "GroupUserGroupId",
                table: "UserApplications");

            migrationBuilder.RenameColumn(
                name: "GroupUserUserId",
                table: "UserApplications",
                newName: "GroupUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GroupUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GroupUserPermissions",
                columns: table => new
                {
                    GroupUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserPermissions", x => new { x.GroupUserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_GroupUserPermissions_GroupUsers_GroupUserId",
                        column: x => x.GroupUserId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserPermissions_PermissionId",
                table: "GroupUserPermissions",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropTable(
                name: "GroupUserPermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupUsers");

            migrationBuilder.RenameColumn(
                name: "GroupUserId",
                table: "UserApplications",
                newName: "GroupUserUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserGroupId",
                table: "UserApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.CreateTable(
                name: "GroupUserPermission",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "int", nullable: false),
                    GroupUsersGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupUsersUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserPermission", x => new { x.PermissionsId, x.GroupUsersGroupId, x.GroupUsersUserId });
                    table.ForeignKey(
                        name: "FK_GroupUserPermission_GroupUsers_GroupUsersGroupId_GroupUsersUserId",
                        columns: x => new { x.GroupUsersGroupId, x.GroupUsersUserId },
                        principalTable: "GroupUsers",
                        principalColumns: new[] { "GroupId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserPermission_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications",
                columns: new[] { "GroupUserGroupId", "GroupUserUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserPermission_GroupUsersGroupId_GroupUsersUserId",
                table: "GroupUserPermission",
                columns: new[] { "GroupUsersGroupId", "GroupUsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications",
                columns: new[] { "GroupUserGroupId", "GroupUserUserId" },
                principalTable: "GroupUsers",
                principalColumns: new[] { "GroupId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
