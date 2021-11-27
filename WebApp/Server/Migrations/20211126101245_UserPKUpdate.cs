using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class UserPKUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_UserAccountInfo_UsersUserId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_UserAccountInfo_owningUserUserId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccountInfo",
                table: "UserAccountInfo");

            migrationBuilder.RenameTable(
                name: "UserAccountInfo",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Users_UsersUserId",
                table: "ProjectUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_owningUserUserId",
                table: "Tickets",
                column: "owningUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Users_UsersUserId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_owningUserUserId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserAccountInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccountInfo",
                table: "UserAccountInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_UserAccountInfo_UsersUserId",
                table: "ProjectUser",
                column: "UsersUserId",
                principalTable: "UserAccountInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_UserAccountInfo_owningUserUserId",
                table: "Tickets",
                column: "owningUserUserId",
                principalTable: "UserAccountInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
