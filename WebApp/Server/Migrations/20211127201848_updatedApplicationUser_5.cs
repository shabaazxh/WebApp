using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class updatedApplicationUser_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_owningUserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "owningUserId",
                table: "Tickets",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_owningUserId",
                table: "Tickets",
                newName: "IX_Tickets_AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedUserId",
                table: "Tickets",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedUserId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "Tickets",
                newName: "owningUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_AssignedUserId",
                table: "Tickets",
                newName: "IX_Tickets_owningUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_owningUserId",
                table: "Tickets",
                column: "owningUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
