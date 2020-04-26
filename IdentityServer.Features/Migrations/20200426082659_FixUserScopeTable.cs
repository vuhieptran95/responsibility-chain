using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Features.Migrations
{
    public partial class FixUserScopeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserScopes_Users_Username1",
                table: "UserScopes");

            migrationBuilder.DropIndex(
                name: "IX_UserScopes_Username1",
                table: "UserScopes");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "UserScopes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScopes_Users_Username",
                table: "UserScopes",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserScopes_Users_Username",
                table: "UserScopes");

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "UserScopes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserScopes_Username1",
                table: "UserScopes",
                column: "Username1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScopes_Users_Username1",
                table: "UserScopes",
                column: "Username1",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
