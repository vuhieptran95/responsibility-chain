using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddStatusToAdditionalInfoIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Issues");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AdditionalInfoIssues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AdditionalInfoIssues");

            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
