using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddUserAndRoleToAuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AuditLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "AuditLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "User",
                table: "AuditLogs");
        }
    }
}
