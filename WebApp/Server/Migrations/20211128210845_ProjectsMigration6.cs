using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class ProjectsMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_UserID",
                table: "ProjectUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectID",
                table: "ProjectUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers");

            migrationBuilder.RenameTable(
                name: "ProjectUsers",
                newName: "projectUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUsers_UserID",
                table: "projectUsers",
                newName: "IX_projectUsers_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projectUsers",
                table: "projectUsers",
                columns: new[] { "ProjectID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_projectUsers_AspNetUsers_UserID",
                table: "projectUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projectUsers_Projects_ProjectID",
                table: "projectUsers",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectUsers_AspNetUsers_UserID",
                table: "projectUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_projectUsers_Projects_ProjectID",
                table: "projectUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projectUsers",
                table: "projectUsers");

            migrationBuilder.RenameTable(
                name: "projectUsers",
                newName: "ProjectUsers");

            migrationBuilder.RenameIndex(
                name: "IX_projectUsers_UserID",
                table: "ProjectUsers",
                newName: "IX_ProjectUsers_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers",
                columns: new[] { "ProjectID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_UserID",
                table: "ProjectUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectID",
                table: "ProjectUsers",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
