using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class ProjectsMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_UsersId",
                table: "ProjectUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUsers_ProjectID",
                table: "ProjectUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUsers_UsersId",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "ProjectUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "ProjectUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers",
                columns: new[] { "ProjectID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserID",
                table: "ProjectUsers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_UserID",
                table: "ProjectUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_UserID",
                table: "ProjectUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectUsers_UserID",
                table: "ProjectUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "ProjectUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "ProjectUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers",
                column: "ProjectUsersKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectID",
                table: "ProjectUsers",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UsersId",
                table: "ProjectUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_AspNetUsers_UsersId",
                table: "ProjectUsers",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
