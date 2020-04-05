using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class ProjectAccessRequiredEmailAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectAccesses_ProjectId_Role_Email",
                table: "ProjectAccesses");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "ProjectAccesses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ProjectAccesses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAccesses_ProjectId_Role_Email",
                table: "ProjectAccesses",
                columns: new[] { "ProjectId", "Role", "Email" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectAccesses_ProjectId_Role_Email",
                table: "ProjectAccesses");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "ProjectAccesses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ProjectAccesses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAccesses_ProjectId_Role_Email",
                table: "ProjectAccesses",
                columns: new[] { "ProjectId", "Role", "Email" },
                unique: true,
                filter: "[Role] IS NOT NULL AND [Email] IS NOT NULL");
        }
    }
}
